﻿using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PDYS.InfraStructure
{
    public class ExecuteCommandAction : TriggerAction<UIElement>
    {
        public static DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ExecuteCommandAction), null);
        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }


        public static DependencyProperty ParameterProperty = DependencyProperty.Register("Parameter", typeof(object), typeof(ExecuteCommandAction), null);
        public object Parameter
        {
            get
            {
                return GetValue(ParameterProperty);
            }
            set
            {
                SetValue(ParameterProperty, value);

            }
        }

        protected override void Invoke(object parameter)
        {
            Command.Execute(Parameter);
        }
    }
}
