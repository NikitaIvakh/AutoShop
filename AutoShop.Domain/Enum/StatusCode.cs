namespace AutoShop.Domain.Enum
{
    public enum StatusCode
    {
        Ok = 200,
        InternalServerError = 500,
        CarNotFound = 10,
        UserNotFound = 0,
        UserAlreadyExists = 1,
        NotFountRoles = 2,
        ProfileIsNull = 11,
        OrderNotFound = 20,
    }
}