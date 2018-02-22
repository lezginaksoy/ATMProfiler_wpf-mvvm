
using ATMDesigner.UI.View;
using ATMDesigner.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace ATMDesigner.UI
{
    internal class ViewModelSelectionService : ViewModelBase
    {
        private ViewModelDesignerCanvas designerCanvas;

        public List<IModelSelectable> currentSelection;
        internal List<IModelSelectable> CurrentSelection
        {
            get
            {
                if (currentSelection == null)
                    currentSelection = new List<IModelSelectable>();
                return currentSelection;
            }
        }
      
        public ViewModelSelectionService(ViewModelDesignerCanvas canvas)
        {
            this.designerCanvas = canvas;      
        }

        internal void SelectItem(IModelSelectable item)
        {
            this.ClearSelection();
            this.AddToSelection(item);
        }

        internal void AddToSelection(IModelSelectable item)
        {
            if (item is IModelGroupable)
            {
                List<IModelGroupable> groupItems = GetGroupMembers(item as IModelGroupable);

                foreach (IModelSelectable groupItem in groupItems)
                {
                    groupItem.IsSelected = true;
                    CurrentSelection.Add(groupItem);
                    MainWindow m = new MainWindow();
                    m.propertyGrid.SelectedObject = null;
                    m.propertyGrid.SelectedObjectName = "";
                }
            }
            else
            {
                item.IsSelected = true;
                CurrentSelection.Add(item);
            }         
        }

        internal void RemoveFromSelection(IModelSelectable item)
        {
            if (item is IModelGroupable)
            {
                List<IModelGroupable> groupItems = GetGroupMembers(item as IModelGroupable);

                foreach (IModelSelectable groupItem in groupItems)
                {
                    groupItem.IsSelected = false;
                    CurrentSelection.Remove(groupItem);
                }
            }
            else
            {
                item.IsSelected = false;
                CurrentSelection.Remove(item);
            }
        }

        internal void ClearSelection()
        {
            CurrentSelection.ForEach(item => item.IsSelected = false);
            CurrentSelection.Clear();
        }

        internal void SelectAll()
        {
            ClearSelection();
            CurrentSelection.AddRange(designerCanvas.Children.OfType<IModelSelectable>());
            CurrentSelection.ForEach(item => item.IsSelected = true);

        }

        internal List<IModelGroupable> GetGroupMembers(IModelGroupable item)
        {
            IEnumerable<IModelGroupable> list = designerCanvas.Children.OfType<IModelGroupable>();
            IModelGroupable rootItem = GetRoot(list, item);
            return GetGroupMembers(list, rootItem);
        }

        internal IModelGroupable GetGroupRoot(IModelGroupable item)
        {
            IEnumerable<IModelGroupable> list = designerCanvas.Children.OfType<IModelGroupable>();
            return GetRoot(list, item);
        }

        private IModelGroupable GetRoot(IEnumerable<IModelGroupable> list, IModelGroupable node)
        {
            if (node == null || node.ParentID == Guid.Empty)
            {
                return node;
            }
            else
            {
                foreach (IModelGroupable item in list)
                {
                    if (item.ID == node.ParentID)
                    {
                        return GetRoot(list, item);
                    }
                }
                return null;
            }
        }

        private List<IModelGroupable> GetGroupMembers(IEnumerable<IModelGroupable> list, IModelGroupable parent)
        {
            List<IModelGroupable> groupMembers = new List<IModelGroupable>();
            groupMembers.Add(parent);

            var children = list.Where(node => node.ParentID == parent.ID);

            foreach (IModelGroupable child in children)
            {
                groupMembers.AddRange(GetGroupMembers(list, child));
            }

            return groupMembers;
        }

    }
}
