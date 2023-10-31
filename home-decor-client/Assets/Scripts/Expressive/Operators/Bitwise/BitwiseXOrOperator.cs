using System;
using Expressive.Expressions;

namespace Expressive.Operators.Bitwise
{
	internal class BitwiseXOrOperator : OperatorBase
	{
		public override string[] Tags
		{
			get
			{
				return new string[]
				{
					"^"
				};
			}
		}

		public override IExpression BuildExpression(Token previousToken, IExpression[] expressions, ExpressiveOptions options)
		{
			return new BinaryExpression(BinaryExpressionType.BitwiseXOr, expressions[0], expressions[1], options);
		}

		public override OperatorPrecedence GetPrecedence(Token previousToken)
		{
			return OperatorPrecedence.BitwiseXOr;
		}
	}
}
