using Flex.Validation;

// ReSharper disable once CheckNamespace
namespace FluentValidation
{
    // ReSharper disable once InconsistentNaming
    internal static class IRuleBuilderOptions
    {
        /// <summary>
        /// Specifies a condition limiting when the validator should run.
        /// The validator will only be executed if object property is not null.
        /// </summary>
        /// <param name="rule">The current rule</param>
        /// <param name="applyConditionTo">Whether the condition should be applied to the current rule or all rules in the chain</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> WhenNotNull<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, ApplyConditionTo applyConditionTo = ApplyConditionTo.AllValidators)
        {
            return rule.Configure(config => config.ApplyCondition(p => p != null, applyConditionTo));
        }

        public static IRuleBuilderOptions<T, TProperty> UnlessIsNullOrEmpty<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, ApplyConditionTo applyConditionTo = ApplyConditionTo.AllValidators)
        {
            return rule.Configure(config =>
                config.ApplyCondition(p =>
                    config.GetPropertyValue(p.InstanceToValidate) is string value && !string.IsNullOrEmpty(value), applyConditionTo));
        }

        /// <summary>
        /// Defines a ansi character validator for the current rule builder that ensures that the specified string is only contains ansi characters.
        /// </summary>
        public static IRuleBuilderOptions<T, string> AnsiCharacters<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new AnsiValidator<T>());
        }
    }
}