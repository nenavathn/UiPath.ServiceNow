
using System;
using System.Activities;
using System.Activities.Statements;
using System.Activities.Validation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace ServiceNow
{
    public static class ActivityConstraints
    {
        public static Constraint HasParentType<TActivity, TParent>(string validationMessage) where TActivity : Activity where TParent : Activity
        {
            return ActivityConstraints.HasParent<TActivity>((Func<Activity, bool>)(p => (object)(p as TParent) != null), validationMessage);
        }

        public static Constraint HasParent<TActivity>(Func<Activity, bool> condition, string validationMessage) where TActivity : Activity
        {
            DelegateInArgument<TActivity> delegateInArgument1 = new DelegateInArgument<TActivity>();
            DelegateInArgument<ValidationContext> delegateInArgument2 = new DelegateInArgument<ValidationContext>();
            Variable<bool> variable = new Variable<bool>();
            DelegateInArgument<Activity> parent = new DelegateInArgument<Activity>();
            Constraint<TActivity> constraint1 = new Constraint<TActivity>();
            Constraint<TActivity> constraint2 = constraint1;
            ActivityAction<TActivity, ValidationContext> activityAction1 = new ActivityAction<TActivity, ValidationContext>();
            activityAction1.Argument1 = delegateInArgument1;
            activityAction1.Argument2 = delegateInArgument2;
            ActivityAction<TActivity, ValidationContext> activityAction2 = activityAction1;
            Sequence sequence1 = new Sequence();
            sequence1.Variables.Add((Variable)variable);
            Collection<Activity> activities = sequence1.Activities;
            ForEach<Activity> forEach1 = new ForEach<Activity>();
            forEach1.Values = (InArgument<IEnumerable<Activity>>)((Activity<IEnumerable<Activity>>)new GetParentChain()
            {
                ValidationContext = (InArgument<ValidationContext>)((DelegateArgument)delegateInArgument2)
            });
            ForEach<Activity> forEach2 = forEach1;
            ActivityAction<Activity> activityAction3 = new ActivityAction<Activity>();
            activityAction3.Argument = parent;
            ActivityAction<Activity> activityAction4 = activityAction3;
            If if1 = new If();
            if1.Condition = new InArgument<bool>((Expression<Func<ActivityContext, bool>>)(ctx => condition(parent.Get(ctx))));
            if1.Then = (Activity)new Assign<bool>()
            {
                Value = (InArgument<bool>)true,
                To = (OutArgument<bool>)((Variable)variable)
            };
            If if2 = if1;
            activityAction4.Handler = (Activity)if2;
            ActivityAction<Activity> activityAction5 = activityAction3;
            forEach2.Body = activityAction5;
            ForEach<Activity> forEach3 = forEach1;
            activities.Add((Activity)forEach3);
            sequence1.Activities.Add((Activity)new AssertValidation()
            {
                Assertion = new InArgument<bool>((Variable)variable),
                Message = new InArgument<string>(validationMessage)
            });
            Sequence sequence2 = sequence1;
            activityAction2.Handler = (Activity)sequence2;
            ActivityAction<TActivity, ValidationContext> activityAction6 = activityAction1;
            constraint2.Body = activityAction6;
            return (Constraint)constraint1;
        }
    }
}
