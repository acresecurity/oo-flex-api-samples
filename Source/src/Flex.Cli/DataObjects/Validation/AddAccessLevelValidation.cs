using Flex.Cli.DataEntry.AccessLevels.Settings;
using Flex.Services.Abstractions;
using FluentValidation;

namespace Flex.DataObjects.Validation
{
    // ReSharper disable once UnusedMember.Global
    internal class AddAccessLevelValidation : AbstractValidator<AddAccessLevelSettings>
    {
        public AddAccessLevelValidation(ICacheStore cacheStore)
        {
            RuleFor(p => p.Name).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.Description).Length(0, 255).UnlessIsNullOrEmpty();

            RuleFor(p => p.Activation)
                .LessThan(p => p.Deactivation)
                .When(p => p.Activation != null && p.Deactivation != null)
                .WithMessage("Activation cannot be greater than deactivation date.");


            RuleFor(p => p.SecurityLevel)
                .IsInEnum()
                .MustAsync(async (accessLevel, securityLevel, context, token) =>
                {
                    var userInfo = await cacheStore.UserInfo();
                    if (userInfo == null)
                        return true; // Fall back to server side validation

                    var restrictedBySecurityLevel = securityLevel switch
                    {
                        SecurityLevel.Normal => userInfo[UserRights.AssignAxsLvl].AsBool(),
                        SecurityLevel.Low => userInfo[UserRights.AssignAxsLvl_Low].AsBool(),
                        SecurityLevel.Medium => userInfo[UserRights.AssignAxsLvl_Medium].AsBool(),
                        SecurityLevel.High => userInfo[UserRights.AssignAxsLvl_High].AsBool(),
                        SecurityLevel.Custom1 => userInfo[UserRights.AssignAxsLvl_Custom1].AsBool(),
                        SecurityLevel.Custom2 => userInfo[UserRights.AssignAxsLvl_Custom2].AsBool(),
                        SecurityLevel.Custom3 => userInfo[UserRights.AssignAxsLvl_Custom3].AsBool(),
                        SecurityLevel.Custom4 => userInfo[UserRights.AssignAxsLvl_Custom4].AsBool(),
                        _ => false
                    };

                    if (!restrictedBySecurityLevel)
                    {
                        context.AddFailure(nameof(AddAccessLevelSettings.SecurityLevel), $"User does not have access to {securityLevel} security level.");
                        return false;
                    }

                    return true;
                })
                .WhenNotNull();
        }
    }
}
