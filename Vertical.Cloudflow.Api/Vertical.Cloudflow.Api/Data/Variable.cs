namespace Vertical.Cloudflow.Api.Data {

    /// <summary>
    /// variable used to specify data in requests
    /// </summary>
    public class Variable {

        /// <summary>
        /// creates a new <see cref="Variable"/>
        /// </summary>
        /// <param name="name">name of variable</param>
        /// <param name="value">variable value</param>
        public Variable(string name, object value) {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// variable name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// value of variable
        /// </summary>
        public object Value { get; }
    }
}