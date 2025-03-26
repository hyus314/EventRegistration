namespace EventRegistration.Common.EmailMessages
{
    public static class EmailSubjectTitles
    {
        public const string WelcomeSubjectTitle =
            "Добре дошли в Кафе Каризма, {0}!";

        //0 -> event type 1 -> full date with bulgarian culture 2 -> client name
        public const string NewEventAddedSubjectTitle =
            "Ново събитие с тип {0} беше резервивано на {1} за името на {2}. Вижте повече детайли.";

        //0 -> client name, 1 -> phone number, 2 -> full start date 
        public const string EventEditedSubjectTitle =
            "Бяха извършени промени върху събитие за името на {0}, {1}, с дата {2}. Вижте повече детайли.";
    }
}
