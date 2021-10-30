namespace Mir2_V2_WebApi.Injection {
    public static class InjectionHandler {
        public static AccountDbInjectionHandler AccountDbInjectionHandler { get; } = new AccountDbInjectionHandler();
    }
}
