﻿namespace Model
{
    public interface IValidator<T>
    {
        bool IsValid(T value);
        bool IsInvalid(T value);
    }

}