﻿using Scryber.Expressive.Expressions;
using System;
using System.Collections.Generic;

namespace Scryber.Expressive.Functions.Conversion
{
    public class IntegerFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name => "Integer";

        public override object Evaluate(IExpression[] parameters, IDictionary<string, object> variables, Context context)
        {
            this.ValidateParameterCount(parameters, 1, 1);

            var objectToConvert = parameters[0].Evaluate(variables);

            // No point converting if there is nothing to convert.
            if (objectToConvert is null) { return null; }

            return Convert.ToInt32(objectToConvert, context.CurrentCulture);
        }

        #endregion
    }
}
