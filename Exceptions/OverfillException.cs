namespace ConsoleApp1.Exceptions;

public class OverfillException(String message = "Maximum payload is exceeded.") : Exception(message);