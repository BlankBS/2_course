using System;
using System.Collections.Generic;

public interface ICollectionOperations<T>
{
    void Add(T item);
    bool Remove(T item);
    IEnumerable<T> Find(Predicate<T> predicate);
    void DisplayAll();
    int Count { get; }
}