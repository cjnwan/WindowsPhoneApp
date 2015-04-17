using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WindowsPhoneApp.CustomControls
{ 
    /// <summary>
    /// Defines how the LoopingSelector communicates with data source.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    public interface ILoopingSelectorDataSource
    {
        /// <summary>
        /// Get the next datum, relative to an existing datum.
        /// </summary>
        /// <param name="relativeTo">The datum the return value will be relative to.</param>
        /// <returns>The next datum.</returns>
        object GetNext(object relativeTo);

        /// <summary>
        /// Get the previous datum, relative to an existing datum.
        /// </summary>
        /// <param name="relativeTo">The datum the return value will be relative to.</param>
        /// <returns>The previous datum.</returns>
        object GetPrevious(object relativeTo);

        /// <summary>
        /// The selected item. Should never be null.
        /// </summary>
        object SelectedItem { get; set; }

        /// <summary>
        /// Raised when the selection changes.
        /// </summary>
        event EventHandler<SelectionChangedEventArgs> SelectionChanged;
    }
}
