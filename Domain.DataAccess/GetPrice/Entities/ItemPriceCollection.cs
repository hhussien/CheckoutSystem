using System;
using System.Collections;

namespace Domain.DataAccess.GetPrice.Entities
{
    /// <summary>
    /// Collection of <see cref="ItemPrice"/>
    /// </summary>
    public sealed class ItemPriceCollection : CollectionBase
    {
       
        #region Methods
        /// <summary>
        /// Adds <see cref="ItemPrice"/> to the collection.
        /// </summary>
        /// <param name="itemPrice"></param>
        /// <returns>The position into which the new element was inserted.</returns>
        /// <remarks>
        /// Adds an <see cref="Item"/> object to the end of the collection.
        /// </remarks>
        public int Add(ItemPrice itemPrice)
        {
            return List.Add(itemPrice);
        }

        /// <summary>
        /// Inserts a <see cref="ItemPrice"/> object into the collection at specified location.
        /// </summary>
        /// <param name="index">The position into which the new element must be inserted.</param>
        /// <param name="itemPrice"></param>
        /// <remarks>
        /// Inserts a <see cref="ItemPrice"/> object object into inner list at specified index.
        /// </remarks>
        public void Insert(int index, ItemPrice itemPrice)
        {
            List.Insert(index, itemPrice);
        }

        /// <summary>
        /// Removes <see cref="ItemPrice"/> object from the collection.
        /// </summary>
        /// <param name="itemPrice"></param>
        /// <remarks>
        /// Removes the first occurrence of a specific <see cref="ItemPrice"/> from the
        /// collection.
        /// </remarks>
        public void Remove(ItemPrice itemPrice)
        {
            List.Remove(itemPrice);
        }

        /// <summary>
        /// Determines whether specified <see cref="Item"/> object is included in 
        /// the collection.
        /// </summary>
        /// <param name="itemPrice"></param>
        /// <returns>
        /// True if provided ItemPrice object exists in the collection. False, otherwise.
        /// </returns>
        /// <remarks>
        /// This method determines equality by calling <see cref="Object.Equals(object)"/>.
        /// </remarks>
        public bool Contains(ItemPrice itemPrice)
        {
            return List.Contains(itemPrice);
        }

        /// <summary>
        /// Returns location of the <see cref="Item"/> object within collection.
        /// </summary>
        /// <param name="itemPrice"></param>
        /// <returns>Index of provided <see cref="Item"/> within collection.</returns>
        /// <remarks>
        /// This method determines equality by calling <see cref="Object.Equals(object)"/>.
        /// </remarks>
        public int IndexOf(ItemPrice itemPrice)
        {
            return List.IndexOf(itemPrice);
        }

        /// <summary>
        /// Copies <see cref="Item"/> objects into array.
        /// </summary>
        /// <param name="array">Destination array</param>
        /// <param name="index">Index</param>
        /// <remarks>
        /// Copies the entire Item collection to a compatible 
        /// one-dimensional Array, starting at the beginning of the target
        /// collection.
        /// </remarks>
        public void CopyTo(ItemPrice[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <summary>
        /// Returns <see cref="Item"/> object located at specified location.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        public ItemPrice this[int index]
        {
            get
            {
                return List[index] as ItemPrice;
            }
            set
            {
                List[index] = value;
            }
        }

       /// <summary>
        /// Gives access to type safe enumerator. Needed to support 
        /// compile time validation of foreach constructs
        /// </summary>
        /// <returns><see cref="ItemPriceEnumerator"/> object</returns>
        /// <remarks>
        /// Returns an enumerator for the entire <see cref="ItemPrice"/> collection.
        /// </remarks>
        new public ItemPriceEnumerator GetEnumerator()
        {
            return new ItemPriceEnumerator(List.GetEnumerator());
        }

        #endregion Methods
    }

    /// <summary>
    /// <see cref="ItemPrice"/> type safe enumerator. 
    /// </summary>
    /// <remarks>
    /// It is used to support compile time type validation when using foreach construct. 
    /// Compile time validation will apply only for code developed in C#.
    /// </remarks>
    public class ItemPriceEnumerator : IEnumerator
    {
        #region Class Members

        private IEnumerator m_enumerator;

        #endregion

        #region Constructors / Destructors

        /// <summary>
        /// Creates instance of ItemPrice enumerator
        /// </summary>
        /// <param name="enumerator">
        /// Enumerator provided by collection. 
        /// All functionality will be delegated to the enumerator.
        /// </param>
        /// <remarks>
        /// ShortcutProfileEnumerator is provided to support strong-typed 
        /// ShortcutProfileCollection and compile-time validation.
        /// </remarks>
        public ItemPriceEnumerator(IEnumerator enumerator)
        {
            m_enumerator = enumerator;
        }

        #endregion

        #region Delegated methods

        /// <summary>
        /// Returns current element for the enumerator
        /// </summary>
        /// <remarks>
        /// After an enumerator is created or after a Reset, MoveNext 
        /// must be called to advance the enumerator to the first element 
        /// of the collection before reading the value of Current; 
        /// otherwise, Current is undefined.
        /// </remarks>
        public ItemPrice Current
        {
            get
            {
                return m_enumerator.Current as ItemPrice;
            }
        }

        /// <summary>
        /// This property returns current element of the enumerator as base object type.
        /// </summary>
        object IEnumerator.Current
        {
            get
            {
                return m_enumerator.Current;
            }
        }

        /// <summary>
        /// Moves next along the list of objects.
        /// </summary>
        /// <returns>
        /// True if the end of the list is not reached. False, otherwise.
        /// </returns>
        /// <remarks>
        /// After an enumerator is created or after a Reset, MoveNext 
        /// must be called to advance the enumerator to the first element 
        /// of the collection before reading the value of Current; 
        /// otherwise, Current is undefined.
        /// </remarks>
        public bool MoveNext()
        {
            return m_enumerator.MoveNext();
        }

        /// <summary>
        /// Resets pointer to the currrent element to the first object in the list.
        /// </summary>
        /// <remarks>
        /// After an enumerator is created or after a Reset, MoveNext 
        /// must be called to advance the enumerator to the first element 
        /// of the collection before reading the value of Current; 
        /// otherwise, Current is undefined.
        /// </remarks>
        public void Reset()
        {
            m_enumerator.Reset();
        }

        #endregion
    }
}
