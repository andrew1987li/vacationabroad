using System;
using System.Collections;

public class Snapshot : ICollection, IEnumerable
{
	private ICollection collection;

	public Snapshot (System.Collections.ICollection Collection)
	{
		collection = Collection;
	}

	#region ICollection Members

	public bool IsSynchronized
	{
		get
		{
			return collection.IsSynchronized;
		}
	}

	public int Count
	{
		get
		{
			return collection.Count;
		}
	}

	public void CopyTo(Array array, int index)
	{
		collection.CopyTo (array, index);
	}

	public object SyncRoot
	{
		get
		{
			return collection.SyncRoot;
		}
	}

	#endregion

	#region IEnumerable Members

	public IEnumerator GetEnumerator()
	{
		return new SnapshotEnumerator (collection);
	}

	#endregion
}

public class SnapshotEnumerator : IEnumerator
{
	private ICollection originalcollection;
	private object[] collection;
	private int curpos;
	private int count;

	public SnapshotEnumerator (ICollection Collection)
	{
		originalcollection = Collection;

		Reset ();
	}

	#region IEnumerator Members

	public void Reset()
	{
		count = originalcollection.Count;
		if (count > 0)
			collection = new object[count];
		else
			collection = null;

		curpos = 0;
		foreach (object obj in originalcollection)
			collection[curpos++] = obj;

		curpos = -1;
	}

	public object Current
	{
		get
		{
			if ((curpos >= 0) && (curpos < count))
				return collection[curpos];
			else
				throw new InvalidOperationException ("No current element at this position in Snapshot collection");
		}
	}

	public bool MoveNext()
	{
		if (curpos < count)
			curpos++;
		return (curpos < count);
	}

	#endregion
}
