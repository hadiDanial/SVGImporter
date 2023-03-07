using SVGImporter.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SVGImporter.Elements.PathUtility
{
    internal abstract class PathCommand
    {
        private bool isAbsolute;
        protected string data;
        protected List<float> values;
        protected const string DATA_PATTERN = "(\\d+\\.\\d+|\\d+)+(([ |,|\\-])*(\\d+\\.\\d+|\\d+))+|";

        public bool IsAbsolute { get => isAbsolute; set => isAbsolute = value; }

        private const char MOVE = 'M';
        private const char LINE = 'L';
        private const char HORIZONTAL = 'H';
        private const char VERTICAL = 'V';
        private const char CLOSE_PATH = 'Z';
        private const char CUBIC_CURVE = 'C';
        private const char CUBIC_CURVE_CONTINUE = 'S';
        private const char QUADRATIC_CURVE = 'Q';
        private const char QUADRATIC_CURVE_CONTINUE = 'T';
        private const char ARC = 'A';

        protected PathCommand(string data, bool isAbsolute)
        {
            this.isAbsolute = isAbsolute;
            this.data = data;
            values = GetDataValues(data);
        }

        public static PathCommand CreateCommand(string commandString)
        {
            if (string.IsNullOrEmpty(commandString)) throw new InvalidDataException($"Invalid data for path: {commandString}");
            char commandLetter = commandString[0];
            bool isAbsolute = false;
            if (!char.IsLetter(commandLetter)) throw new InvalidDataException($"Invalid data for path: {commandString}");
            if (char.IsUpper(commandLetter)) isAbsolute = true;
            commandLetter = char.ToUpper(commandLetter);
            PathCommand command = null;
            string commandData = commandString.Substring(1);
            switch (commandLetter)
            {
                case MOVE:
                    {
                        command = new MoveCommand(commandData, isAbsolute);
                        break;
                    }
                case LINE:
                    {
                        command = new LineCommand(commandData, isAbsolute);
                        break;
                    }
                case HORIZONTAL:
                    {
                        command = new SimpleMoveCommand(commandData, isAbsolute, SimpleMoveCommand.SimpleMoveType.Horizontal);
                        break;
                    }
                case VERTICAL:
                    {
                        command = new SimpleMoveCommand(commandData, isAbsolute, SimpleMoveCommand.SimpleMoveType.Vertical);
                        break;
                    }
                    case CLOSE_PATH:
                    {
                        command = new ClosePathCommand(commandData, isAbsolute);
                        break;
                    }
                case CUBIC_CURVE:
                    {
                        command = new CubicCurveCommand(commandData, isAbsolute);
                        break;
                    }
                case CUBIC_CURVE_CONTINUE:
                    {
                        command = new CubicCurveContinueCommand(commandData, isAbsolute);
                        break;
                    }
                case QUADRATIC_CURVE:
                    {
                        command = new QuadraticCurveCommand(commandData, isAbsolute);
                        break;
                    }
                case QUADRATIC_CURVE_CONTINUE:
                    {
                        command = new QuadraticCurveContinueCommand(commandData, isAbsolute);
                        break;
                    }
                case ARC:
                    {
                        command = new ArcCommand(commandData, isAbsolute);
                        break;
                    }
                default:
                    throw new InvalidDataException($"Invalid command for path: {commandString}");
            }
            return command;
        }

        protected static List<float> GetDataValues(string data)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<string> matches = new List<string>();

            for (int i = 0; i < data.Length; i++)
            {
                char ch = data[i];
                if (char.IsDigit(ch) || ch.Equals('.'))
                    stringBuilder.Append(ch);
                else if (ch.Equals(' ') || ch.Equals(','))
                {
                    if(stringBuilder.Length > 0) matches.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
                else if(ch.Equals('-'))
                {
                    if (stringBuilder.Length > 0) matches.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                    stringBuilder.Append(ch);
                }                
            }
            if (stringBuilder.Length > 0) matches.Add(stringBuilder.ToString());

            List<float> values = new List<float>();
            foreach (var item in matches)
            {
                float val = 0;
                if(float.TryParse(item, out val))
                    values.Add(val);
            }
            return values;
        }
    }
}
