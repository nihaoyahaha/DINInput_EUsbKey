//******************************************************************************
// ユーザ名					ニューコン
// システム名				
// サブシステム名			演算式計算
// 作成者					蒋@NC
// 改版日					2009/01/23
// 改版内容					新規作成
//******************************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;

namespace DI.NCFrameWork
{
    public class CalculateExpression
    {
        private class NCSymbol : System.Collections.IComparer
        {
            public String Token;
            public TOKENCLASS Cls;
            public PRECEDENCE PrecedenceLevel;
            public String tag;

            public delegate int compare_function(Object x, Object y);

            /// <summary>
            /// シンボルを比較する
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(Object x, Object y)
            {
                NCSymbol asym, bsym;
                asym = (NCSymbol)x;
                bsym = (NCSymbol)y;

                if (asym.Token.CompareTo(bsym.Token) > 0) return 1;

                if (asym.Token.CompareTo(bsym.Token) < 0) return -1;

                if ((int)asym.PrecedenceLevel == -1 || (int)bsym.PrecedenceLevel == -1) return 0;

                if ((int)asym.PrecedenceLevel > (int)bsym.PrecedenceLevel) return 1;

                if ((int)asym.PrecedenceLevel < (int)bsym.PrecedenceLevel) return -1;

                return 0;

            }
        }

        /// <summary>
        /// 演算子のレベル
        /// </summary>
        private enum PRECEDENCE
        {
            NONE = 0,
            LEVEL0 = 1,
            LEVEL1 = 2,
            LEVEL2 = 3,
            LEVEL3 = 4,
            LEVEL4 = 5,
            LEVEL5 = 6
        }

        /// <summary>
        /// トケンの列挙型
        /// </summary>
        private enum TOKENCLASS
        {
            KEYWORD = 1,
            IDENTIFIER = 2,
            NUMBER = 3,
            OPERATOR = 4,
            PUNCTUATION = 5
        }

        #region private 変数
        private ArrayList m_tokens;
        private int[,] m_State;
        private String m_colstring;
        private const String ALPHA = "_ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const String DIGITS = "#0123456789";

        private String[] m_funcs = {"sin", "cos", "tan", "arcsin", "arccos", 
                                     "arctan", "sqrt", "max", "min", "floor", 
                                     "ceiling", "log", "log10", 
                                     "ln", "round", "abs", "neg", "pos"};

        private ArrayList m_operators;
        // 演算子のTOKEN配列
        private Stack m_stack = new Stack();
        #endregion

        /// <summary>
        /// 演算式を初期化する
        /// </summary>
        private void init_operators()
        {
            NCSymbol op;

            m_operators = new ArrayList();

            op = new NCSymbol();
            op.Token = "-";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL1;
            m_operators.Add(op);

            op = new NCSymbol();
            op.Token = "+";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL1;
            m_operators.Add(op);

            op = new NCSymbol();
            op.Token = "*";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL2;
            m_operators.Add(op);

            op = new NCSymbol();
            op.Token = "/";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL2;
            m_operators.Add(op);

            op = new NCSymbol();
            op.Token = @"\";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL2;
            m_operators.Add(op);

            op = new NCSymbol();
            op.Token = "%";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL2;
            m_operators.Add(op);

            op = new NCSymbol();
            op.Token = "^";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL3;
            m_operators.Add(op);

            op = new NCSymbol();
            op.Token = "!";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL5;
            m_operators.Add(op);

            op = new NCSymbol();
            op.Token = "&";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL5;
            m_operators.Add(op);

            op = new NCSymbol();
            op.Token = "-";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL4;
            m_operators.Add(op);

            op = new NCSymbol();
            op.Token = "+";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL4;
            m_operators.Add(op);

            op = new NCSymbol();
            op.Token = "(";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL5;
            m_operators.Add(op);

            op = new NCSymbol();
            op.Token = ")";
            op.Cls = TOKENCLASS.OPERATOR;
            op.PrecedenceLevel = PRECEDENCE.LEVEL0;
            m_operators.Add(op);

            m_operators.Sort(op);
        }

        /// <summary>
        /// 演算式を計算する
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public double evaluate(String expression)
        {
            Queue symbols = new Queue();
            try
            {
                if (IsNumeric(expression))
                {
                    return Double.Parse(expression);
                }

                calc_scan(expression, ref symbols);

                return level0(symbols);
            }
            catch (Exception exp)
            {
                throw exp;
                // MessageBox.Show("Invalid expression");
            }
        }

        /// <summary>
        /// 指定した文字列は数字であるかどうかを判明する
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        private bool IsNumeric(string strNumber)
        {
            bool bReturn = false;
            Regex regNotNumberPattern = new Regex("[^0-9.-]");
            Regex regTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex regTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex regNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            bReturn = !regNotNumberPattern.IsMatch(strNumber) &&
                !regTwoDotPattern.IsMatch(strNumber) &&
                !regTwoMinusPattern.IsMatch(strNumber) &&
                regNumberPattern.IsMatch(strNumber);

            return bReturn;
        }

        /// <summary>
        /// 演算する
        /// </summary>
        /// <param name="op"></param>
        /// <param name="operand1"></param>
        /// <param name="operand2"></param>
        /// <returns></returns>
        private double calc_op(NCSymbol op, double operand1, double operand2)
        {

            switch (op.Token.ToLower())
            {

                case "&":
                    return 5;
                case "^":
                    return ((long)operand1 ^ (long)operand2);
                case "+":

                    switch (op.PrecedenceLevel)
                    {
                        case PRECEDENCE.LEVEL1:
                            return (operand2 + operand1);
                        case PRECEDENCE.LEVEL4:
                            return operand1;
                    }
                    break;
                case "-":
                    switch (op.PrecedenceLevel)
                    {
                        case PRECEDENCE.LEVEL1:
                            return (operand1 - operand2);
                        case PRECEDENCE.LEVEL4:
                            return -1 * operand1;
                    }
                    break;
                case "*":
                    return (operand2 * operand1);
                case "/":
                    return (operand1 / operand2);
                case @"\":
                    return (long)(operand1) / (long)(operand2);
                case "%":
                    return (operand1 % operand2);
                case "!":
                    int i = 0;
                    double res = 1;

                    if (operand1 > 1)
                    {
                        for (i = (int)operand1; i <= 1; i--)
                        {
                            res = res * i;
                        }

                    }
                    return (res);
            }
            return 0;
        }

        /// <summary>
        /// 特定の関数を演算する
        /// </summary>
        /// <param name="func"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private double calc_function(String func, ArrayList args)
        {

            switch (func.ToLower())
            {
                case "cos":
                    return (Math.Cos((double)(args[1])));
                case "sin":
                    return (Math.Sin((double)(args[1])));
                case "tan":
                    return (Math.Tan((double)(args[1])));
                case "floor":
                    return (Math.Floor((double)(args[1])));
                case "ceiling":
                    return (Math.Ceiling((double)(args[1])));
                case "max":
                    return (Math.Max((double)(args[1]), (double)(args[2])));
                case "min":
                    return (Math.Min((double)(args[1]), (double)(args[2])));
                case "arcsin":
                    return (Math.Asin((double)(args[1])));
                case "arccos":
                    return (Math.Acos((double)(args[1])));
                case "arctan":
                    return (Math.Atan((double)(args[1])));
                case "sqrt":
                    return (Math.Sqrt((double)(args[1])));
                case "log":
                    return (Math.Log10((double)(args[1])));
                case "log10":
                    return (Math.Log10((double)(args[1])));
                case "abs":
                    return (Math.Abs((double)(args[1])));
                case "round":
                    return (Math.Round((double)(args[1])));
                case "ln":
                    return (Math.Log((double)(args[1])));
                case "neg":
                    return (-1 * (double)(args[1]));
                case "pos":
                    return (+1 * (double)(args[1]));
            }
            return 0;

        }

        /// <summary>
        /// 特定の演算子の値を返す
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private double identifier(String token)
        {
            switch (token.ToLower())
            {
                case "e":
                    return Math.E;
                case "pi":
                    return Math.PI;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// 指定した演算子は正かどうかを判明する
        /// </summary>
        /// <param name="token"></param>
        /// <param name="level"></param>
        /// <param name="operator1"></param>
        /// <returns></returns>
        private bool is_operator(String token, PRECEDENCE level, ref NCSymbol operator1)
        {
            try
            {
                NCSymbol op = new NCSymbol();
                op.Token = token;
                op.PrecedenceLevel = level;
                op.tag = "test";

                int ir = m_operators.BinarySearch(op, op);

                if (ir > -1)
                {
                    operator1 = (NCSymbol)(m_operators[ir]);
                    return true;
                }

                return false;

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 演算子は特定関数であるかどうかを判明する
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool is_function(String token)
        {
            try
            {
                int lr = Array.BinarySearch(m_funcs, token.ToLower());

                return (lr > -1);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 演算式を分析する
        /// </summary>
        /// <param name="line"></param>
        /// <param name="symbols"></param>
        /// <returns></returns>
        public bool calc_scan(String line, ref Queue symbols)
        {
            int sp;
            int cp;
            int col;
            int lex_state;
            char cc;

            symbols = new Queue();

            line = line + " ";
            sp = 0;
            cp = 0;
            lex_state = 1;


            while (cp <= line.Length - 1)
            {
                cc = line[cp];

                col = m_colstring.IndexOf(cc) + 3;

                if (col == 2)
                {
                    if (ALPHA.IndexOf(Char.ToUpper(cc)) > 0)
                    {
                        col = 1;
                    }
                    else if (DIGITS.IndexOf(Char.ToUpper(cc)) > 0)
                    {
                        col = 2;
                    }
                    else
                    {
                        col = 6;
                    }
                }
                else if (col > 5)
                {
                    col = 7;
                }

                lex_state = m_State[lex_state - 1, col - 1];

                switch (lex_state)
                {
                    case 3:
                        {
                            NCSymbol sym = new NCSymbol();

                            sym.Token = line.Substring(sp, cp - sp);
                            if (is_function(sym.Token))
                            {
                                sym.Cls = TOKENCLASS.KEYWORD;
                            }
                            else
                            {
                                sym.Cls = TOKENCLASS.IDENTIFIER;
                            }

                            symbols.Enqueue(sym);

                            lex_state = 1;
                            cp = cp - 1;
                        }
                        break;
                    case 5:
                        {
                            NCSymbol sym = new NCSymbol();

                            sym.Token = line.Substring(sp, cp - sp);
                            sym.Cls = TOKENCLASS.NUMBER;

                            symbols.Enqueue(sym);

                            lex_state = 1;
                            cp = cp - 1;
                        }
                        break;
                    case 6:
                        {
                            NCSymbol sym = new NCSymbol();

                            sym.Token = line.Substring(sp, cp - sp + 1);
                            sym.Cls = TOKENCLASS.PUNCTUATION;

                            symbols.Enqueue(sym);

                            lex_state = 1;
                        }
                        break;
                    case 7:
                        {
                            NCSymbol sym = new NCSymbol();

                            sym.Token = line.Substring(sp, cp - sp + 1);
                            sym.Cls = TOKENCLASS.OPERATOR;

                            symbols.Enqueue(sym);

                            lex_state = 1;
                        }
                        break;

                }

                cp += 1;
                if (lex_state == 1)
                {
                    sp = cp;
                }
            }
            return true;
        }

        /// <summary>
        /// 演算式計算を初期化する
        /// </summary>
        private void init()
        {
            NCSymbol op;

            int[,] state = {{2, 4, 1, 1, 4, 6, 7}, 
                           {2, 3, 3, 3, 3, 3, 3}, 
                           {1, 1, 1, 1, 1, 1, 1}, 
                           {2, 4, 5, 5, 4, 5, 5}, 
                           {1, 1, 1, 1, 1, 1, 1}, 
                           {1, 1, 1, 1, 1, 1, 1}, 
                           {1, 1, 1, 1, 1, 1, 1}};

            init_operators();

            m_State = state;
            m_colstring = (char)9 + " " + ".()";
            foreach (object obj in m_operators)
            {
                op = (NCSymbol)obj;
                m_colstring = m_colstring + op.Token;
            }

            Array.Sort(m_funcs);
            m_tokens = new ArrayList();
        }

        /// <summary>
        /// 演算式計算のインスタンスを実装する
        /// </summary>
        public CalculateExpression()
        {
            init();
        }

        /// <summary>
        /// 演算式のレベル1を返す
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private double level0(Queue tokens)
        {
            return level1(tokens);
        }

        /// <summary>
        /// レベル1の素数返す
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private double level1_prime(Queue tokens, Double result)
        {
            NCSymbol symbol, operator1;

            if (tokens.Count > 0)
            {
                symbol = (NCSymbol)(tokens.Peek());
            }
            else
            {
                return result;
            }

            operator1 = new NCSymbol();
            if (is_operator(symbol.Token, PRECEDENCE.LEVEL1, ref operator1))
            {
                tokens.Dequeue();
                result = calc_op(operator1, result, level2(tokens));
                result = level1_prime(tokens, result);

            }


            return result;

        }

        private Double level1(Queue tokens)
        {
            return level1_prime(tokens, level2(tokens));
        }

        private Double level2(Queue tokens)
        {
            return level2_prime(tokens, level3(tokens));
        }

        private Double level2_prime(Queue tokens, Double result)
        {

            NCSymbol symbol, operator1;

            if (tokens.Count > 0)
            {
                symbol = (NCSymbol)(tokens.Peek());
            }
            else
            {
                return result;
            }
            operator1 = new NCSymbol();
            if (is_operator(symbol.Token, PRECEDENCE.LEVEL2, ref operator1))
            {
                tokens.Dequeue();
                result = calc_op(operator1, result, level3(tokens));
                result = level2_prime(tokens, result);

            }

            return result;

        }

        private Double level3(Queue tokens)
        {
            return level3_prime(tokens, level4(tokens));
        }

        private Double level3_prime(Queue tokens, Double result)
        {

            NCSymbol symbol, operator1;

            if (tokens.Count > 0)
            {
                symbol = (NCSymbol)(tokens.Peek());
            }
            else
            {
                return result;
            }

            operator1 = new NCSymbol();
            if (is_operator(symbol.Token, PRECEDENCE.LEVEL3, ref operator1))
            {
                tokens.Dequeue();
                result = calc_op(operator1, result, level4(tokens));
                result = level3_prime(tokens, result);

            }

            return result;

        }

        private Double level4(Queue tokens)
        {
            return level3_prime(tokens, level5(tokens));
        }

        private Double level4_prime(Queue tokens, Double result)
        {

            NCSymbol symbol, operator1;

            if (tokens.Count > 0)
            {
                symbol = (NCSymbol)(tokens.Peek());
            }
            else
            {
                return result;
            }

            operator1 = new NCSymbol();
            if (is_operator(symbol.Token, PRECEDENCE.LEVEL4, ref operator1))
            {
                tokens.Dequeue();
                result = calc_op(operator1, result, level5(tokens));
                result = level4_prime(tokens, result);

            }

            return result;

        }

        private Double level5(Queue tokens)
        {
            return level5_prime(tokens, level6(tokens));
        }

        private Double level5_prime(Queue tokens, Double result)
        {

            NCSymbol symbol, operator1;

            if (tokens.Count > 0)
            {
                symbol = (NCSymbol)(tokens.Peek());
            }
            else
            {
                return result;
            }
            operator1 = new NCSymbol();
            if (is_operator(symbol.Token, PRECEDENCE.LEVEL5, ref operator1))
            {
                tokens.Dequeue();
                result = calc_op(operator1, result, level6(tokens));
                result = level5_prime(tokens, result);

            }

            return result;

        }


        private double level6(Queue tokens)
        {
            NCSymbol symbol;

            if (tokens.Count > 0)
            {
                symbol = (NCSymbol)(tokens.Peek());
            }
            else
            {
                throw new System.Exception("Invalid expression.");
            }

            Double val;

            if (symbol.Token == "(")
            {
                tokens.Dequeue();
                val = level0(tokens);

                symbol = (NCSymbol)(tokens.Dequeue());

                if (symbol.Token != ")")
                {
                    throw new System.Exception("Invalid expression.");
                }
                return val;
            }
            else
            {

                switch (symbol.Cls)
                {

                    case TOKENCLASS.IDENTIFIER:
                        tokens.Dequeue();
                        return identifier(symbol.Token);

                    case TOKENCLASS.KEYWORD:
                        tokens.Dequeue();
                        return calc_function(symbol.Token, arguments(tokens));
                    case TOKENCLASS.NUMBER:

                        tokens.Dequeue();
                        m_stack.Push(Double.Parse(symbol.Token));
                        return Double.Parse(symbol.Token);

                    default:
                        throw new System.Exception("Invalid expression.");
                }

            }
        }

        private ArrayList arguments(Queue tokens)
        {
            NCSymbol symbol;
            ArrayList args = new ArrayList();

            if (tokens.Count > 0)
            {
                symbol = (NCSymbol)(tokens.Peek());
            }
            else
            {
                throw new System.Exception("Invalid expression.");
            }

            if (symbol.Token == "(")
            {
                tokens.Dequeue();
                args.Add(level0(tokens));

                symbol = (NCSymbol)(tokens.Dequeue());
                while (symbol.Token != ")")
                {
                    if (symbol.Token == ",")
                    {
                        args.Add(level0(tokens));
                    }
                    else
                    {
                        throw new System.Exception("Invalid expression.");
                    }
                    symbol = (NCSymbol)(tokens.Dequeue());
                }

                return args;
            }
            else
            {
                throw new System.Exception("Invalid expression.");

            }

        }

    }
}
