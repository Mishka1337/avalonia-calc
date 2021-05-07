using System;
using System.Reactive;
using Calculator.Core;
using Calculator.UI.Models;
using ReactiveUI;

namespace Calculator.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";
        private readonly ExpressionParser _parser;
        private readonly Interpreter _interpreter;

        private string _mathExpression = "";

        public string MathExpression
        {
            get => _mathExpression;
            set => this.RaiseAndSetIfChanged(ref _mathExpression, value);
        }

        public ReactiveCommand<int, Unit> AddNumberCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveLastSymbolCommand {get;}
        public ReactiveCommand<Operation, Unit> AddOperationCommand { get; }
        public ReactiveCommand<Unit, Unit> AddDotCommand { get; }

        public MainWindowViewModel(Interpreter interpreter, ExpressionParser parser)
        {
            _interpreter = interpreter;
            _parser = parser;
            AddNumberCommand = ReactiveCommand.Create<int>(AddNumber);
            RemoveLastSymbolCommand = ReactiveCommand.Create(RemoveLastSymbol);
            AddDotCommand = ReactiveCommand.Create(AddDot);
            AddOperationCommand = ReactiveCommand.Create<Operation>(AddOperataion);
            RxApp.DefaultExceptionHandler = Observer.Create<Exception>(
                ex => Console.Write("next"),
                ex => Console.Write("Unhandled RxUI error"));
        }

        private bool IsExpressionEndsWithDigit
        {
            get
            {
                if (string.IsNullOrWhiteSpace(MathExpression))
                {
                    return false;
                }
                return char.IsDigit(MathExpression[^1]);
            }
        }
            

        private void AddNumber(int value)
        {
            MathExpression = MathExpression + value.ToString();
        }

        private void AddDot()
        {
            if (IsExpressionEndsWithDigit)
            {
                MathExpression = MathExpression + ",";
            }
        }

        private void RemoveLastSymbol()
        {
            if (MathExpression.Length >= 1)
            {
                MathExpression = MathExpression.Remove(MathExpression.Length - 1);
            }
        }

        private void AddOperataion(Operation operation)
        {
            if (!IsExpressionEndsWithDigit)
            {
                return;
            }
            switch (operation)
            {
                case Operation.Add:
                {
                    MathExpression = MathExpression + "+";
                    break;
                }
                case Operation.Subtract:
                {
                    MathExpression = MathExpression + "-";
                    break;
                } 
                case Operation.Multiply:
                {
                    MathExpression = MathExpression + "*";
                    break;
                }
                case Operation.Divide:
                {
                    MathExpression = MathExpression + "/";
                    break;
                }
                case Operation.Result:
                {
                    MathExpression = CalculateExpression(MathExpression);
                    break;
                }
            }
        }

        private string CalculateExpression(string expressionString)
        {
            var parsedExpression = _parser.Parse(expressionString);
            if (parsedExpression == null)
            {
                return String.Empty;
            }
            var result = _interpreter.Interpret(parsedExpression);
            if (result == null)
            {
                return string.Empty;
            }
            return result;
        } 
    }
}