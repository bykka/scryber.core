﻿using Scryber.Expressive.Expressions;
using System;
using System.Collections.Generic;

namespace Scryber.Expressive.Functions.Mathematical
{
    public class AtanFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Atan"; } }

        public override object Evaluate(IExpression[] parameters, IDictionary<string, object> variables, Context context)
        {
            this.ValidateParameterCount(parameters, 1, 1);

            return Math.Atan(Convert.ToDouble(parameters[0].Evaluate(variables)));
        }

        #endregion
    }
}
