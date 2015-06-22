using System;
using System.Linq.Expressions;

namespace CoreBase.Entities
{
    /// <summary> Get from http://stackoverflow.com/questions/18976495/entity-framework-linq-to-entities-only-supports-casting-edm-primitive-or-enumer" </summary>
    public sealed class EntityCastRemoverVisitor<TEntity,TInterface> : ExpressionVisitor
        where TEntity : TInterface
    {
        /// <summary> Convert predicate </summary>
        /// <param name="predicate">Exspression for convert</param>
        /// <returns>Converted expression</returns>
        public static Expression<Func<TEntity, bool>> Convert(Expression<Func<TEntity, bool>> predicate)
        {
            var visitor = new EntityCastRemoverVisitor<TEntity, TInterface>();

            var visitedExpression = visitor.Visit(predicate);

            return (Expression<Func<TEntity, bool>>)visitedExpression;
        }

        /// <summary> Visits the children of the <see cref="T:System.Linq.Expressions.UnaryExpression"/>. </summary>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
        /// </returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType == ExpressionType.Convert && typeof(TInterface).IsAssignableFrom(node.Type))
            {
                return node.Operand;
            }

            return base.VisitUnary(node);
        }
    }
}
