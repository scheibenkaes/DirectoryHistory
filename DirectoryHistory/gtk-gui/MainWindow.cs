// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------



public partial class MainWindow {
    
    private Gtk.UIManager UIManager;
    
    private Gtk.Action openAction;
    
    private Gtk.Action FileAction;
    
    private Gtk.Action HistoryAction;
    
    private Gtk.Action InfoAction;
    
    private Gtk.Action refreshAction;
    
    private Gtk.Action quitAction;
    
    private Gtk.VBox vbox1;
    
    private Gtk.MenuBar menubar1;
    
    private Gtk.VBox vbox2;
    
    private Gtk.Toolbar toolbar1;
    
    private DirectoryHistory.UI.FolderList folderlist;
    
    private Gtk.Statusbar statusbar1;
    
    protected virtual void Build() {
        Stetic.Gui.Initialize(this);
        // Widget MainWindow
        this.UIManager = new Gtk.UIManager();
        Gtk.ActionGroup w1 = new Gtk.ActionGroup("Default");
        this.openAction = new Gtk.Action("openAction", Mono.Unix.Catalog.GetString("openDirectory"), null, "gtk-open");
        this.openAction.ShortLabel = Mono.Unix.Catalog.GetString("openDirectory");
        w1.Add(this.openAction, null);
        this.FileAction = new Gtk.Action("FileAction", Mono.Unix.Catalog.GetString("File"), null, null);
        this.FileAction.ShortLabel = Mono.Unix.Catalog.GetString("File");
        w1.Add(this.FileAction, null);
        this.HistoryAction = new Gtk.Action("HistoryAction", Mono.Unix.Catalog.GetString("History"), null, null);
        this.HistoryAction.ShortLabel = Mono.Unix.Catalog.GetString("History");
        w1.Add(this.HistoryAction, null);
        this.InfoAction = new Gtk.Action("InfoAction", Mono.Unix.Catalog.GetString("Info"), null, null);
        this.InfoAction.ShortLabel = Mono.Unix.Catalog.GetString("Info");
        w1.Add(this.InfoAction, null);
        this.refreshAction = new Gtk.Action("refreshAction", Mono.Unix.Catalog.GetString("refresh"), null, "gtk-refresh");
        this.refreshAction.ShortLabel = Mono.Unix.Catalog.GetString("refresh");
        w1.Add(this.refreshAction, null);
        this.quitAction = new Gtk.Action("quitAction", Mono.Unix.Catalog.GetString("quit"), null, "gtk-quit");
        this.quitAction.ShortLabel = Mono.Unix.Catalog.GetString("quit");
        w1.Add(this.quitAction, null);
        this.UIManager.InsertActionGroup(w1, 0);
        this.AddAccelGroup(this.UIManager.AccelGroup);
        this.Name = "MainWindow";
        this.Title = Mono.Unix.Catalog.GetString("MainWindow");
        this.WindowPosition = ((Gtk.WindowPosition)(4));
        // Container child MainWindow.Gtk.Container+ContainerChild
        this.vbox1 = new Gtk.VBox();
        this.vbox1.Name = "vbox1";
        this.vbox1.Spacing = 6;
        // Container child vbox1.Gtk.Box+BoxChild
        this.UIManager.AddUiFromString("<ui><menubar name='menubar1'><menu name='FileAction' action='FileAction'><menuitem name='quitAction' action='quitAction'/></menu><menu name='HistoryAction' action='HistoryAction'/><menu name='InfoAction' action='InfoAction'/></menubar></ui>");
        this.menubar1 = ((Gtk.MenuBar)(this.UIManager.GetWidget("/menubar1")));
        this.menubar1.Name = "menubar1";
        this.vbox1.Add(this.menubar1);
        Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.vbox1[this.menubar1]));
        w2.Position = 0;
        w2.Expand = false;
        w2.Fill = false;
        // Container child vbox1.Gtk.Box+BoxChild
        this.vbox2 = new Gtk.VBox();
        this.vbox2.Name = "vbox2";
        this.vbox2.Spacing = 6;
        // Container child vbox2.Gtk.Box+BoxChild
        this.UIManager.AddUiFromString("<ui><toolbar name='toolbar1'><toolitem name='openAction' action='openAction'/><toolitem name='refreshAction' action='refreshAction'/><toolitem name='quitAction' action='quitAction'/></toolbar></ui>");
        this.toolbar1 = ((Gtk.Toolbar)(this.UIManager.GetWidget("/toolbar1")));
        this.toolbar1.Name = "toolbar1";
        this.toolbar1.ShowArrow = false;
        this.toolbar1.ToolbarStyle = ((Gtk.ToolbarStyle)(0));
        this.toolbar1.IconSize = ((Gtk.IconSize)(3));
        this.vbox2.Add(this.toolbar1);
        Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.vbox2[this.toolbar1]));
        w3.Position = 0;
        w3.Expand = false;
        w3.Fill = false;
        // Container child vbox2.Gtk.Box+BoxChild
        this.folderlist = new DirectoryHistory.UI.FolderList();
        this.folderlist.Events = ((Gdk.EventMask)(256));
        this.folderlist.Name = "folderlist";
        this.vbox2.Add(this.folderlist);
        Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(this.vbox2[this.folderlist]));
        w4.Position = 1;
        this.vbox1.Add(this.vbox2);
        Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.vbox1[this.vbox2]));
        w5.Position = 1;
        // Container child vbox1.Gtk.Box+BoxChild
        this.statusbar1 = new Gtk.Statusbar();
        this.statusbar1.Name = "statusbar1";
        this.statusbar1.Spacing = 6;
        this.vbox1.Add(this.statusbar1);
        Gtk.Box.BoxChild w6 = ((Gtk.Box.BoxChild)(this.vbox1[this.statusbar1]));
        w6.Position = 2;
        w6.Expand = false;
        w6.Fill = false;
        this.Add(this.vbox1);
        if ((this.Child != null)) {
            this.Child.ShowAll();
        }
        this.DefaultWidth = 462;
        this.DefaultHeight = 300;
        this.Show();
        this.DeleteEvent += new Gtk.DeleteEventHandler(this.OnDeleteEvent);
        this.openAction.Activated += new System.EventHandler(this.OnOpenActionActivated);
        this.refreshAction.Activated += new System.EventHandler(this.OnRefreshActionActivated);
        this.quitAction.Activated += new System.EventHandler(this.OnQuitActionActivated);
    }
}
