# GenericList

A custom implementation of the List<T> data structure in C# built from scratch.

This project recreates many of the core functionalities provided by the .NET List<T> collection while focusing on understanding how dynamic arrays, generic programming, and collection interfaces work internally.

---

## 📖 Project Overview

GenericList<T> is a custom dynamic array implementation that supports adding, removing, inserting, searching, sorting, enumeration, and more.

The project was built to strengthen understanding of:

- Data Structures
- Generics
- Collections
- Interfaces
- Enumerators
- Memory Management
- Object-Oriented Programming

Instead of relying on the built-in List<T>, all core behaviors are manually implemented.

---

## ✨ Features

### Collection Operations

- Add()
- AddRange()
- Insert()
- InsertRange()
- Remove()
- RemoveAt()
- RemoveRange()
- RemoveAll()
- Clear()

### Searching

- IndexOf()
- Contains()

### Utility Methods

- Sort()
- Reverse()
- ToArray()
- CopyTo()

### Dynamic Array Management

- Automatic resizing
- Capacity management
- Internal array expansion

### Indexers

- Access items by index
- Access multiple items using integer indexes
- Access multiple items using string indexes

### Generic Support

- Works with any data type
- Supports custom equality comparers

### Enumeration

- IEnumerable<T>
- IEnumerator<T>

Allows:

```csharp
foreach(var item in list)
{
    Console.WriteLine(item);
}
```

### Undo Feature

- Undo last removed item

---

## 🛠 Technologies Used

- C#
- .NET
- Generics
- Collections
- Interfaces
- Data Structures
- Object-Oriented Programming

---

## 📚 What I Learned

During this project I learned:

- How List<T> works internally.
- Dynamic array resizing strategies.
- Implementing IList<T>.
- Implementing ICollection<T>.
- Implementing IEnumerable<T>.
- Working with Generic Types.
- Creating custom indexers.
- Using Equality Comparers.
- Building custom iterators using yield return.
- Managing memory manually with arrays.
- Designing reusable and maintainable code.
- Applying OOP principles.
- Building custom data structures from scratch.

---

## 🎯 Challenges

Some challenges faced during development:

- Implementing dynamic resizing correctly.
- Handling generic types safely.
- Supporting custom equality comparers.
- Building enumerators.
- Managing insertions and deletions efficiently.
- Maintaining array consistency after shifting elements.

---

## 🚀 Future Improvements

Possible future enhancements:

- Binary Search
- Find()
- FindAll()
- Predicate Support
- Capacity Property
- Shrink-to-fit functionality
- Unit Tests
- Performance Benchmarking

---

## 📂 Implemented Interfaces

```csharp
IList<T>
ICollection<T>
IEnumerable<T>
```

---

## 👨‍💻 Author

Hamza Eldeeb

Passionate about Computer Science fundamentals, Data Structures, Algorithms, OOP, and Backend Development using C#.

---

## ⭐ Project Purpose

This project was built for educational purposes to understand how generic collections work internally and to improve software engineering, problem-solving, and data structure implementation skills.
