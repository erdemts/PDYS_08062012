using System;

namespace Mvvm.Messenger
{
    /// <summary>
    /// Attribute to decorate a method to be registered to the Mediator
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class MessageProviderSinkAttribute : Attribute
    {
        /// <summary>
        /// The message to register to 
        /// </summary>
		public string Message { get; private set; }

        /// <summary>
        /// The type of parameter for the Method
        /// </summary>
		public Type ParameterType { get; set; }

        /// <summary>
        /// Constructs a method
        /// </summary>
        /// <param name="message">The message to subscribe to</param>
        public MessageProviderSinkAttribute(string message)
        {
            Message = message;
        }
    }
}