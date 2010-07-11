// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace DirectoryHistory.UI {
    
    
    public partial class HistoryEntry {
        
        private Gtk.Table table1;
        
        private Gtk.Alignment alignment1;
        
        private Gtk.Button openButton;
        
        private Gtk.Label dateLabel;
        
        private Gtk.ScrolledWindow GtkScrolledWindow;
        
        private Gtk.TextView commentTextview;
        
        private Gtk.Label label1;
        
        private Gtk.Label label3;
        
        private Gtk.Label label4;
        
        private Gtk.ScrolledWindow scrolledDifference;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget DirectoryHistory.UI.HistoryEntry
            Stetic.BinContainer.Attach(this);
            this.Name = "DirectoryHistory.UI.HistoryEntry";
            // Container child DirectoryHistory.UI.HistoryEntry.Gtk.Container+ContainerChild
            this.table1 = new Gtk.Table(((uint)(4)), ((uint)(2)), false);
            this.table1.Name = "table1";
            this.table1.RowSpacing = ((uint)(6));
            this.table1.ColumnSpacing = ((uint)(6));
            // Container child table1.Gtk.Table+TableChild
            this.alignment1 = new Gtk.Alignment(0.5F, 0.5F, 1F, 1F);
            this.alignment1.Name = "alignment1";
            // Container child alignment1.Gtk.Container+ContainerChild
            this.openButton = new Gtk.Button();
            this.openButton.CanFocus = true;
            this.openButton.Name = "openButton";
            this.openButton.UseUnderline = true;
            this.openButton.Label = Mono.Unix.Catalog.GetString("Open this version");
            this.alignment1.Add(this.openButton);
            this.table1.Add(this.alignment1);
            Gtk.Table.TableChild w2 = ((Gtk.Table.TableChild)(this.table1[this.alignment1]));
            w2.TopAttach = ((uint)(3));
            w2.BottomAttach = ((uint)(4));
            w2.LeftAttach = ((uint)(1));
            w2.RightAttach = ((uint)(2));
            w2.XOptions = ((Gtk.AttachOptions)(0));
            w2.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.dateLabel = new Gtk.Label();
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.LabelProp = Mono.Unix.Catalog.GetString("label2");
            this.table1.Add(this.dateLabel);
            Gtk.Table.TableChild w3 = ((Gtk.Table.TableChild)(this.table1[this.dateLabel]));
            w3.LeftAttach = ((uint)(1));
            w3.RightAttach = ((uint)(2));
            w3.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.GtkScrolledWindow = new Gtk.ScrolledWindow();
            this.GtkScrolledWindow.Name = "GtkScrolledWindow";
            this.GtkScrolledWindow.ShadowType = ((Gtk.ShadowType)(1));
            // Container child GtkScrolledWindow.Gtk.Container+ContainerChild
            this.commentTextview = new Gtk.TextView();
            this.commentTextview.CanFocus = true;
            this.commentTextview.Name = "commentTextview";
            this.commentTextview.Editable = false;
            this.GtkScrolledWindow.Add(this.commentTextview);
            this.table1.Add(this.GtkScrolledWindow);
            Gtk.Table.TableChild w5 = ((Gtk.Table.TableChild)(this.table1[this.GtkScrolledWindow]));
            w5.TopAttach = ((uint)(1));
            w5.BottomAttach = ((uint)(2));
            w5.LeftAttach = ((uint)(1));
            w5.RightAttach = ((uint)(2));
            w5.XOptions = ((Gtk.AttachOptions)(4));
            w5.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label1 = new Gtk.Label();
            this.label1.Name = "label1";
            this.label1.LabelProp = Mono.Unix.Catalog.GetString("Date");
            this.table1.Add(this.label1);
            Gtk.Table.TableChild w6 = ((Gtk.Table.TableChild)(this.table1[this.label1]));
            w6.XOptions = ((Gtk.AttachOptions)(4));
            w6.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label3 = new Gtk.Label();
            this.label3.Name = "label3";
            this.label3.LabelProp = Mono.Unix.Catalog.GetString("Comment");
            this.table1.Add(this.label3);
            Gtk.Table.TableChild w7 = ((Gtk.Table.TableChild)(this.table1[this.label3]));
            w7.TopAttach = ((uint)(1));
            w7.BottomAttach = ((uint)(2));
            w7.XOptions = ((Gtk.AttachOptions)(4));
            w7.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label4 = new Gtk.Label();
            this.label4.Name = "label4";
            this.label4.LabelProp = Mono.Unix.Catalog.GetString("Difference");
            this.table1.Add(this.label4);
            Gtk.Table.TableChild w8 = ((Gtk.Table.TableChild)(this.table1[this.label4]));
            w8.TopAttach = ((uint)(2));
            w8.BottomAttach = ((uint)(3));
            w8.XOptions = ((Gtk.AttachOptions)(4));
            w8.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.scrolledDifference = new Gtk.ScrolledWindow();
            this.scrolledDifference.CanFocus = true;
            this.scrolledDifference.Name = "scrolledDifference";
            this.scrolledDifference.ShadowType = ((Gtk.ShadowType)(1));
            this.table1.Add(this.scrolledDifference);
            Gtk.Table.TableChild w9 = ((Gtk.Table.TableChild)(this.table1[this.scrolledDifference]));
            w9.TopAttach = ((uint)(2));
            w9.BottomAttach = ((uint)(3));
            w9.LeftAttach = ((uint)(1));
            w9.RightAttach = ((uint)(2));
            w9.XOptions = ((Gtk.AttachOptions)(4));
            w9.YOptions = ((Gtk.AttachOptions)(4));
            this.Add(this.table1);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.Hide();
        }
    }
}
