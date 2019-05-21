namespace Vertical.Cloudflow.Api.Data {

    /// <summary>
    /// parameter used in request queries
    /// </summary>
    public class Parameter {

        /// <summary>
        /// creates a new <see cref="Parameter"/>
        /// </summary>
        /// <param name="name">parameter name</param>
        /// <param name="value">parameter value</param>
        public Parameter(string name, string value) {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// name of parameter
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// parameter value
        /// </summary>
        public string Value { get; set; }
    }
}