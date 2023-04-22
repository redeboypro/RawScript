using System.Collections.Generic;
using System.Text;

namespace RawScript
{
    public static class Lexer
    {
        public static bool IsOperator(this char source)
        {
            switch (source)
            {
                case '=' :
                case '*' :
                case '/' :
                case '%' :
                case '+' :
                case '-' :
                case '(' :
                case ')' :
                case '{' :
                case '}' :
                case ';' :
                    return true;
            }

            return false;
        }
        
        public static string[] SeparateLine(string source)
        {
            var tokenList = new List<string>();
            var symIndex = 0;
            while (symIndex < source.Length)
            {
                var sym = source[symIndex];
                if (sym != Shell.TokenSeparator)
                {
                    var token = new StringBuilder();
                    while (sym != Shell.TokenSeparator && symIndex < source.Length)
                    {
                        token.Append(sym);
                        symIndex++;
                        if (symIndex < source.Length)
                        {
                            sym = source[symIndex];
                        }
                    }
                    tokenList.Add(token.ToString());
                }
                symIndex++;
            }

            return tokenList.ToArray();
        }
        
        public static string[] Separate(string source)
        {
            var tokenStringBuilder = new StringBuilder();
            var tokenList = new List<string>();

            foreach (var sym in source)
            {
                var token = tokenStringBuilder.ToString();

                if (sym == Shell.TokenSeparator)
                {
                    if (token.Length > 0)
                    {
                        tokenList.Add(token);
                    }

                    tokenStringBuilder.Clear();
                    continue;
                }
                
                if (sym.IsOperator())
                {
                    if (token.Length > 0)
                    {
                        tokenList.Add(token);
                    }
                    
                    tokenStringBuilder.Clear();
                    tokenList.Add(sym.ToString());
                    continue;
                }
                
                if (!char.IsControl(sym))
                {
                    tokenStringBuilder.Append(sym.ToString());
                }
            }
            
            if (tokenStringBuilder.Length > 0)
            {
                tokenList.Add(tokenStringBuilder.ToString());
            }
            
            return tokenList.ToArray();
        }
    }
}