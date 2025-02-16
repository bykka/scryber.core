﻿using System.Collections.Generic;
using Scryber.Expressive.Expressions;

namespace Scryber.Expressive.Functions.Conversion
{
    public class StringFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name => "String";

        public override object Evaluate(IExpression[] parameters, IDictionary<string, object> variables, Context context)
        {
            this.ValidateParameterCount(parameters, -1, 1);

            var objectToConvert = parameters[0].Evaluate(variables);

            // No point converting if there is nothing to convert.
            if (objectToConvert is null) { return null; }
            
            // Safely check for a format parameter.
            if (parameters.Length > 1)
            {
                var format = parameters[1].Evaluate(variables);

                if (format is string formatString)
                {
                    return string.Format(context.CurrentCulture, $"{{0:{formatString}}}", objectToConvert);
                }
            }

            return objectToConvert.ToString();
        }

        #endregion
    }
}
