using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Button = System.Windows.Controls.Button;
using ListBox = System.Windows.Controls.ListBox;
using TextBox = System.Windows.Controls.TextBox;

namespace ScriptWorkbenchPortable
{
    public class Display : Window
    {
        private const string SearchPattern = "*.rs";
        private new const int Width = 800;
        private new const int Height = 600;

        private readonly ListBox scriptList;

        public Display()
        {
            base.Width = Width;
            base.Height = Height;
            Title = "ScriptWorkbench";

            var scripts = new Dictionary<string, string>();
            var projectFolderDialog = new FolderBrowserDialog();
            var result = projectFolderDialog.ShowDialog();
            if (result is System.Windows.Forms.DialogResult.OK)
            {
                var files = Directory.GetFiles(projectFolderDialog.SelectedPath, SearchPattern, SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    scripts.Add(file, File.ReadAllText(file));
                }
            }
            
            var grid = new Grid();
            
            var inputBox = new TextBox {
                FontFamily = new FontFamily("Consolas"),
                FontSize = 16,
                
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                
                AcceptsReturn = true,
            };
            
            scriptList = new ListBox();
            foreach (var script in scripts)
            {
                scriptList.Items.Add(script.Key);
            }
            scriptList.SelectionChanged += (sender, args) =>
            {
                inputBox.Text = scripts[GetCurrentScriptPath()];
            };
            
            var applyButton = new Button
            {
                Content = "Apply"
            };
            applyButton.Click += (sender, args) =>
            {
                scripts[GetCurrentScriptPath()] = inputBox.Text;
            };
            
            var saveProjectButton = new Button
            {
                Content = "Save All"
            };
            saveProjectButton.Click += (sender, args) =>
            {
                foreach (var script in scripts)
                {
                    File.WriteAllText(script.Key, script.Value);
                }
            };
            
            var toolBar = new StackPanel
            {
                Children =
                {
                    scriptList, applyButton, saveProjectButton
                }
            };

            var splitter = new GridSplitter
            {
                ResizeBehavior = GridResizeBehavior.PreviousAndNext,
                Width = 5,
                ResizeDirection = GridResizeDirection.Columns
            };

            var leftColumn = new ColumnDefinition();
            var splitterColumn = new ColumnDefinition()
            {
                Width = GridLength.Auto
            };
            var rightColumn = new ColumnDefinition();
            
            grid.ColumnDefinitions.Add(leftColumn);
            grid.ColumnDefinitions.Add(splitterColumn);
            grid.ColumnDefinitions.Add(rightColumn);
            
            Grid.SetColumn(toolBar, 0);
            Grid.SetColumn(splitter, 1);
            Grid.SetColumn(inputBox, 2);

            grid.Children.Add(toolBar);
            grid.Children.Add(splitter);
            grid.Children.Add(inputBox);

            Content = grid;
        }

        public string GetCurrentScriptPath()
        {
            return scriptList.SelectedItem.ToString();
        }
    }
}