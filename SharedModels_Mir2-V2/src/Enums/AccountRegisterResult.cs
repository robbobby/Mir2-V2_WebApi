namespace SharedModels_Mir2_V2.Enums {
    public enum AccountRegisterResult {
        UnknownError,
        Success,
        EmailAlreadyExists,
        EmailNotValid,
        InProgress,
        ConnectionError,
        ProtocolError,
        DataProcessingError,
        UserNameAlreadyExists,
        PasswordDoesNotMatchCriteria,
    }
}
