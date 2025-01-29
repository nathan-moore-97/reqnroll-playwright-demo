using System.Collections.Concurrent;
using Microsoft.Playwright;

public sealed class TabStackService
{
    private readonly ConcurrentStack<IPage> _stack = new ConcurrentStack<IPage>();

    // Singleton
    private static readonly TabStackService instance = new TabStackService();

    static TabStackService() { }
    private TabStackService() { }

    public static TabStackService Instance
    {
        get
        {
            return instance;
        }
    }


    /// <summary>
    /// Pushes an IPage onto the stack.
    /// </summary>
    /// <param name="page">The IPage to add to the stack.</param>
    public void Push(IPage page)
    {
        if (page == null)
            throw new ArgumentNullException(nameof(page));

        _stack.Push(page);
    }

    /// <summary>
    /// Attempts to pop an IPage from the stack.
    /// </summary>
    /// <param name="page">The popped IPage, if successful.</param>
    /// <returns>True if an IPage was popped; otherwise, false.</returns>
    public bool TryPop(out IPage page)
    {
        return _stack.TryPop(out page);
    }

    /// <summary>
    /// Attempts to peek at the top IPage on the stack without removing it.
    /// </summary>
    /// <param name="page">The IPage at the top of the stack, if successful.</param>
    /// <returns>True if an IPage was peeked; otherwise, false.</returns>
    public bool TryPeek(out IPage page)
    {
        return _stack.TryPeek(out page);
    }

    /// <summary>
    /// Returns the number of IPage objects in the stack.
    /// </summary>
    public int Count => _stack.Count;

    /// <summary>
    /// Clears all IPage objects from the stack.
    /// </summary>
    public void Clear()
    {
        _stack.Clear();
    }
}