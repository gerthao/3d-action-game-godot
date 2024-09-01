using System;

namespace dActionGame.Asset.Scripts.Monads;

public abstract class Maybe<T>
{
    public static   Maybe<T>       Lift(T value) => value == null ? None() : Some(value);
    public static   Maybe<T>       Some(T value) => new Some<T>(value);
    public static   Maybe<T>       None()        => new None<T>();
    public abstract Maybe<TResult> SelectMany<TResult>(Func<T, Maybe<TResult>> func);
    public abstract Maybe<TResult> Select<TResult>(Func<T, TResult>            func);

    public static Maybe<T> Cond(bool condition, T value) => condition ? new Some<T>(value) : new None<T>();

    public abstract Maybe<TU> Bind<TU>(Func<T, Maybe<TU>> func);
    public abstract Maybe<TU> Map<TU>(Func<T, TU>         func);
    public abstract void      ForEach(Action<T>           action);
    public abstract Maybe<T>  Filter(Func<T, bool>        func);

    public abstract Maybe<T> Find(Func<T, bool> func);

    public abstract Maybe<T> Tap(Action<T> action);

    public abstract T Get();
    public abstract T GetOrElse(T defaultValue);

    public abstract bool IsEmpty();
    public abstract bool IsDefined();
}

public class Some<T> : Maybe<T>
{
    private readonly T _value;

    public Some(T value) => _value = value;

    public override Maybe<TU> Bind<TU>(Func<T, Maybe<TU>> func) => func(_value);

    public override Maybe<TU> Map<TU>(Func<T, TU> func) => new Some<TU>(func(_value));

    public override Maybe<TResult> SelectMany<TResult>(Func<T, Maybe<TResult>> func) => func(_value);

    public override Maybe<TResult> Select<TResult>(Func<T, TResult> func) => new Some<TResult>(func(_value));


    public override void ForEach(Action<T> action)
    {
        action(_value);
    }

    public override Maybe<T> Filter(Func<T, bool> func) => func(_value) ? new Some<T>(_value) : new None<T>();

    public override Maybe<T> Find(Func<T, bool> func) => Filter(func); // same as Filter in Maybe
    public override Maybe<T> Tap(Action<T> action)
    {
        action(_value);

        return new Some<T>(_value);
    }

    public override T Get() => _value;

    public override T GetOrElse(T defaultValue) => _value;

    public override bool IsEmpty() => false;

    public override bool IsDefined() => true;
}

public class None<T> : Maybe<T>
{
    public override Maybe<T>  Tap(Action<T> action)             => new None<T>();
    public override T         Get()                             => throw new InvalidOperationException();
    public override Maybe<TU> Bind<TU>(Func<T, Maybe<TU>> func) => new None<TU>();

    public override Maybe<TU> Map<TU>(Func<T, TU> func) => new None<TU>();

    public override Maybe<TResult> SelectMany<TResult>(Func<T, Maybe<TResult>> func) => new None<TResult>();

    public override Maybe<TResult> Select<TResult>(Func<T, TResult> func) => new None<TResult>();


    public override void ForEach(Action<T> action)
    {
    }

    public override Maybe<T> Filter(Func<T, bool> func) => new None<T>();

    public override Maybe<T> Find(Func<T, bool> func) => new None<T>();

    public override T GetOrElse(T defaultValue) => defaultValue;

    public override bool IsEmpty() => true;

    public override bool IsDefined() => false;
}
