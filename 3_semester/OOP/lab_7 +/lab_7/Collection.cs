using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using System.Runtime.InteropServices;
using System.Diagnostics;

public class CollectionType<T> : ICollectionOperations<T> where T : Animal
{
    private List<T> _collection;

    public int Count => _collection.Count;

    public CollectionType()
    {
        _collection = new List<T>();
    }

    public CollectionType(IEnumerable<T> initialData)
    {
        _collection = new List<T>(initialData);
    }

    public static CollectionType<T> operator +(CollectionType<T> collection, T item)
    {
        collection.Add(item);
        return collection;
    }

    public static CollectionType<T> operator -(CollectionType<T> collection, T item)
    {
        collection.Remove(item);
        return collection;
    }

    public static bool operator ==(CollectionType<T> left, CollectionType<T> right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }
        if (left is null || right is null)
        {
            return false;
        }
        return left._collection.SequenceEqual(right._collection);
    }

    public static bool operator !=(CollectionType<T> left, CollectionType<T> right)
    {
        return !(left == right);
    }

    public void Add(T item)
    {
        try
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Cannot add null animal");
            }

            _collection.Add(item);
            Console.WriteLine($"Animal '{item.Name}' added successfully. Total animals: {_collection.Count}");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Error adding animal: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while adding animal: {ex.Message}");
            throw;
        }
        finally
        {
            Console.WriteLine("Add operation completed");
        }
    }

    public bool Remove(T item)
    {
        try
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Cannot remove null animal");

            bool removed = _collection.Remove(item);
            if (removed)
            {
                Console.WriteLine($"Animal '{item.Name}' removed successfully. Total animals: {_collection.Count}");
            }
            else
            {
                Console.WriteLine($"Animal '{item.Name}' not found in collection");
            }
            return removed;
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Error removing animal: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while removing animal: {ex.Message}");
            return false;
        }
        finally
        {
            Console.WriteLine("Remove operation completed");
        }
    }

    public IEnumerable<T> Find(Predicate<T> predicate)
    {
        try
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null");

            var results = _collection.FindAll(predicate);
            Console.WriteLine($"Found {results.Count} animals matching the predicate");
            return results;
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Error in search: {ex.Message}");
            return Enumerable.Empty<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error during search: {ex.Message}");
            return Enumerable.Empty<T>();
        }
        finally
        {
            Console.WriteLine("Search operation completed");
        }
    }

    public void DisplayAll()
    {
        try
        {
            if (_collection.Count == 0)
            {
                Console.WriteLine("Collection is empty");
                return;
            }

            Console.WriteLine($"\nCollection contains {_collection.Count} animals:");
            Console.WriteLine(new string('=', 50));

            for (int i = 0; i < _collection.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_collection[i]}");
            }
            Console.WriteLine(new string('=', 50));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error displaying collection: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Display operation completed");
        }
    }

    public T this[int index]
    {
        get
        {
            try
            {
                return _collection[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"Index {index} is out of range");
                throw;
            }
        }
        set
        {
            try
            {
                _collection[index] = value;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"Index {index} is out of range");
                throw;
            }
        }
    }

    public void SaveToJson(string filePath)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string json = JsonSerializer.Serialize(_collection, options);
            File.WriteAllText(filePath, json);
            Console.WriteLine($"Collection saved to JSON file: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving to JSON: {ex.Message}");
            throw;
        }
    }

    public void LoadFromJson(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            string json = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var loadedCollection = JsonSerializer.Deserialize<List<T>>(json, options);

            if (loadedCollection != null)
            {
                _collection = loadedCollection;
                Console.WriteLine($"Collection loaded from JSON file: {filePath}. Loaded {_collection.Count} animals");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error loading from JSON: {ex.Message}");
            throw;
        }
    }

    public void SaveToXml(string filePath)
    {
        StreamWriter writer = null;
        try
        {
            var serializer = new XmlSerializer(typeof(List<Animal>), new Type[]
            {
            typeof(Lion), typeof(Tiger), typeof(Owl), typeof(Parrot),
            typeof(Shark), typeof(Crocodile), typeof(Mammal),
            typeof(Bird), typeof(Fish)
            });

            writer = new StreamWriter(filePath);
            var animals = _collection.Cast<Animal>().ToList();
            serializer.Serialize(writer, animals);
            Console.WriteLine($"Collection saved to XML file: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving to XML: {ex.Message}");
            throw;
        }
        finally
        {
            writer?.Dispose();
        }
    }

    public void LoadFromXml(string filePath)
    {
        StreamReader reader = null;
        try
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            var serializer = new XmlSerializer(typeof(List<Animal>), new Type[]
            {
            typeof(Lion), typeof(Tiger), typeof(Owl), typeof(Parrot),
            typeof(Shark), typeof(Crocodile), typeof(Mammal),
            typeof(Bird), typeof(Fish)
            });

            reader = new StreamReader(filePath);
            var loadedCollection = (List<Animal>)serializer.Deserialize(reader);

            if (loadedCollection != null)
            {
                _collection = loadedCollection.OfType<T>().ToList();
                Console.WriteLine($"Collection loaded from XML file: {filePath}. Loaded {_collection.Count} animals");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading from XML: {ex.Message}");
            throw;
        }
        finally
        {
            reader?.Dispose();
        }
    }

    public void SaveToText(string filePath)
    {
        StreamWriter writer = null;
        try
        {
            writer = new StreamWriter(filePath);
            writer.WriteLine($"Animal Collection - Total: {_collection.Count}");
            writer.WriteLine(new string('=', 40));

            foreach(var animal in _collection)
            {
                writer.WriteLine(animal.ToString());
            }
            Console.WriteLine($"Collection saved to text file: {filePath}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error saving to text file: {filePath}");
            throw;
        }
        finally
        {
            writer?.Dispose();
        }
    }

    public override bool Equals(object obj)
    {
        return obj is CollectionType<T> other && this == other;
    }

    public override int GetHashCode()
    {
        return _collection?.GetHashCode() ?? 0;
    }
}