﻿// <copyright file="FloorsTableSource.cs" company="Esri, Inc">
//     Copyright (c) Esri. All rights reserved.
// </copyright>
// <author>Mara Stoica</author>
namespace IndoorNavigation.iOS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Foundation;
    using UIKit;

    /// <summary>
    /// Class handling the source data for the floors TableView 
    /// </summary>
    internal class FloorsTableSource : UITableViewSource
    {
        /// <summary>
        /// The items in the table.
        /// </summary>
        private readonly IEnumerable<string> items;

        /// <summary>
        /// The cell identifier.
        /// </summary>
        private readonly string cellIdentifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:IndoorNavigation.iOS.FloorsTableSource"/> class.
        /// </summary>
        /// <param name="items">Table Items.</param>
        internal FloorsTableSource(IEnumerable<string> items)
        {
            if (items != null)
            {
                this.items = items;
                this.cellIdentifier = "cell_id";
            }
            else
            {
                // TODO: throw null exception
            }
        }

        /// <summary>
        /// Occurs when table row selected.
        /// </summary>
        public event EventHandler<TableRowSelectedEventArgs<string>> TableRowSelected;

        /// <summary>
        /// Called by the TableView to determine how many cells to create for that particular section.
        /// </summary>
        /// <returns>The rows in section.</returns>
        /// <param name="tableview">Containing Tableview.</param>
        /// <param name="section">Specific Section.</param>
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            try
            {
                return this.items.Count();
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Called by the TableView to get the actual UITableViewCell to render for the particular row
        /// </summary>
        /// <returns>The cell.</returns>
        /// <param name="tableView">Table view.</param>
        /// <param name="indexPath">Index path.</param>
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(this.cellIdentifier);

            // If there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, this.cellIdentifier);
            }

            try
            {
                var item = this.items.ElementAt(indexPath.Row);

                var label = (UILabel)cell.ContentView.ViewWithTag(10);
                label.Text = item;

                return cell;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Event for user selecting a floor level
        /// </summary>
        /// <param name="tableView">Table view.</param>
        /// <param name="indexPath">Index path.</param>
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            this.OnTableRowSelected(indexPath);
        }

        /// <summary>
        /// Get the tableview item the user selected and call event handler
        /// </summary>
        /// <param name="itemIndexPath">Item index path.</param>
        private void OnTableRowSelected(NSIndexPath itemIndexPath)
        {
            try
            {
                var item = this.items.ElementAt(itemIndexPath.Row);
                this.TableRowSelected?.Invoke(this, new TableRowSelectedEventArgs<string>(item, itemIndexPath));
            }
            catch
            {
                // TODO: figure out how to trigger this catch and what happens
            }
        }
    }
}
