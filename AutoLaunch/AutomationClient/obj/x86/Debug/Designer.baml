   M S B A M L   `   `   `  ����M  IAutomationClient, Version=1.0.0.112, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient  5   	   G COdyssey, Version=1.3.16.22071, Culture=neutral, PublicKeyToken=nullD/clr-namespace:Odyssey.Controls;assembly=OdysseyOdyssey.Controls 5   	       AutomationClient.Designer   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:AutomationClient     5   	   Imy@http://schemas.microsoft.com/winfx/2006/xaml/presentation/ribbon  5   	   )myodhttp://schemas.odyssey.com/wpf  5   	   9odc/clr-namespace:Odyssey.Controls;assembly=Odyssey  5   	   g cXceed.Wpf.AvalonDock.Themes.Aero, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4[ WXceed.Wpf.AvalonDock, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f49xcad,http://schemas.xceed.com/wpf/xaml/avalondock   5   	     � Title$  Auto Launch Client��5	   	   $
��1364��5
   	   $	��849��5   	    � Icon$> 8Images\Apps-preferences-desktop-launch-feedback-icon.png��5   	      	FontStyle#���   5   	    � WindowState$ 	Maximized=�5   	    � 	Resources 5      .   (AutomationClient.RadioBoolToIntConverter  radioBoolToIntConverter  MessageResultsImages ��Name �� Triggers �� Value �� Binding �� Path	 �� 
TargetName
 �� Property �� Value �� 
TargetType �� UpdateSourceTrigger��%�  &	       &	      &	        5   
   6(   �� 5   
   .R�5      �� $ StatusImage��6    5      �� 5      $ 0��6>    6   �� $ Status�.a�5      �� $	 StatusImage��6   
 � ��$ Images\passPic.png��6H   5      �� 5      $ 1��6>    6   �� $ Status�.a�5      �� $	 StatusImage��6   
 � ��$ Images\failPic.png��6H   5      �� 5      $ 2��6>    6   �� $ Status�.a�5       �� $	 StatusImage��6   
 � ��$ Images\infoPic.png��6H   5!      5#      �� 5%   
   " �6"   .�5&      �� 
 � �� 6+   �� *
FontFamily+$ PropertyChanged=��� 5'      
 � �� 6)   �� *
FontSize+$ PropertyChanged=�5(      5)      .��5+      �  � RowDefinitions 5,   
   �� 5-      $
��Auto �6   �� 5.      $
��642* �6   �� 5/      $	��25* �6   .+�51   
   .  (Microsoft.Windows.Controls.Ribbon.Ribbon  $��Stretch=�6   $��Stretch=�52      ���	#FF749CCD   ApplicationMenu 54      =  7Microsoft.Windows.Controls.Ribbon.RibbonApplicationMenu  55      .@�5W      A  ;Microsoft.Windows.Controls.Ribbon.RibbonApplicationMenuItem  -   5          Name$ saveScriptBtn��5W   3    m� CommandParameter$ 
SaveScript��5Y   3   $��Save Script��5Z   3   6  0Microsoft.Windows.Controls.Ribbon.RibbonMenuItem   ImageSource$ Images\Save-Script.png��5[   3    m� Command 5X   3   �� *SetToolBarAction+5[   3     5\      $ SaveTest��5]   3   $��	Save Test��5^   3   $ Images\Save-Test.png��5_   3    5\   3   �� *SetToolBarAction+5_   3     5`      $ 	SaveSuite��5a   3   $��
Save Suite��5b   3   $ Images\Save-Test.png��5c   3    5`   3   �� *SetToolBarAction+5c   3     5d      $ ExportSuite��5e   3   $��Export Suite��5f   3   $ Images\export-icon.png��5g   3    5d   3   �� *SetToolBarAction+5g   3     5h      -   5        $ exitMenuBtn��5h   3   $
��Exit��5j   3   $* $Images\Actions-dialog-close-icon.png��5k   3     5l      $	��100��63   {�.  6@   5m      .@�5o      1  +Microsoft.Windows.Controls.Ribbon.RibbonTab  -   5        $��HomeTab��5o      $��75��5p      $
��Home��5q      .@�5r      3  -Microsoft.Windows.Controls.Ribbon.RibbonGroup  ���	#FFBED0E7$
��Edit��68   .@�5t      4  .Microsoft.Windows.Controls.Ribbon.RibbonButton  -   5        $��Clear��5t   &   $��ClearAllEntities��5v   &      Label$ Clear��5w   &      LargeImageSource$ Images\newPic.png��5x   &   ��5u   &   �� *SetToolBarAction+5x   &     5y      -   5        $��
deleteStep��5y   &   $��delStep��5|   &   $ Delete��5}   &   $  Images\Sign-Error-icon.png��5~   &   ��5{   &   �� *SetToolBarAction+5~   &     5      -   5        $��
RefreshBtn��5   &   $��RefreshFiles��5�   &   $ Refresh��5�   &   $ Images\Refresh-icon.png��5�   &   ��5�   &   �� *SetToolBarAction+5�   &     5�      -   5        $��	Duplicate��5�   &   $��DuplicateStepEntity��5�   &   $ 	Duplicate��5�   &   $ Images\paste-icon.png��5�   &   ��5�   &   �� *SetToolBarAction+��5�      �� 5�       �� Triggers 5�   "   �� 5�   &    62   �� *SelectedStepEntity+ 6Y   Z� .a�5�   *   �� 
 � {� .  6P   5�   '   5�      5�        5�      -   5        $��CutStep��5�   &   $	 Cut��5�   &   $ Images\Edit-Cut-icon.png��5�   &     5�      -	   5        $��CopyStep��5�   &   $
 Copy��5�   &   $ Images\Copy-icon-step.png��5�   &     5�      -
   5        $��	PasteStep��5�   &   $��	pasteStep��5�   &   $ Paste��5�   &   $  Images\Paste-icon-Step.png��5�   &   ��5�   &   �� *SetToolBarAction+5�   &     5�      $��
UndoScript��5�   &   $
 Undo��5�   &   $ Images\Undo-icon.png��5�   &   ��5�   &   �� *SetToolBarAction+5�   &   5�        5�      ���	#FFBED0E7$��Arrange��68   .@�5�        -   5        $��moveUp��5�   &   $��MoveElementUp��5�   &   $ Up��5�   &   $ Images\Up-32.png��5�   &   ��5�   &   �� *SetToolBarAction+5�   &     5�      -   5        $��moveDw��5�   &   $��MoveElementDown��5�   &   $
 Down��5�   &   $ Images\down-32.png��5�   &   ��5�   &   �� *SetToolBarAction+5�   &   5�        5�      ���	#FFBED0E7$��Server��68   .@�5�        $��ServerConnect��5�   &   $ Connect��5�   &   $- 'Images\Actions-network-connect-icon.png��5�   &   ��5�   &   �� *SetToolBarAction+5�   &     5�      $��ServerLaunch��5�   &   $ Activate��5�   &   $! Images\Home-Server-icon.png��5�   &   ��5�   &   �� *SetToolBarAction+5�   &   5�        5�      ���	#FFBED0E7$��Execute��68   .@�5�      :	  4Microsoft.Windows.Controls.Ribbon.RibbonToggleButton	  -   5        $��debugBtn��5�   ,    	  Label$ DebugOn��5�   ,    	  LargeImageSource$ Images\Red-Ball-icon.png��5�   ,   ��5�   ,   �� *SetToolBarAction+5�   ,     5�      $��PauseExecution��5�   &   $ Pause��5�   &   $ Images\pause-icon.png��5�   &   ��5�   &   �� *SetToolBarAction+��5�      �� 5�       5�   "   �� 5�   &    62   �� *ServerConnect+ 6T   Z� .a�5�   *   �� 
 � {� .  6P   5�   '   5�      5�        5�      $��StopExecution��5�   &   $
 Stop��5�   &   $ Images\stop-icon.png��5�   &   ��5�   &   �� *SetToolBarAction+��5�      �� 5�       5�   "   �� 5�   &    62   �� *ServerConnect+ 6T   Z� .a�5�   *   �� 
 � {� .  6P   5�   '   5�      5�        5�      -   5        $��runBtn��5�   &   $��ExecuteTest��5�   &   $	 Run��5�   &   $ Images\play-icon.png��5�   &   ��5�   &   �� *SetToolBarAction+��5�      �� 5�       5�   "   �� 5�   &    62   �� *ServerConnect+ 6T   Z� .a�5�   *   �� 
 � {� .  6P   5�   '   �� 5�   &    62   �� *SelectedSuiteName+ 6X   Z� .a�5�   *   �� 
 � {� .  6P   5�   '   5�      5�      �� 5�       �� Orientation$ 
Horizontal=�6!   .+�5�      �� $��Cycles:    ��6$   �� 5�      $��60��6"   $��20��5�   "   ��5�   "   �� $ Cycles�$ PropertyChanged=�5      5       5     ���	#FFBED0E7$��
On Failure��68   .@�5     	� $��	Skip Test��6"    d� 	IsChecked 66   �� $ OnFailerAction� �� 	Converter# [  �� ConverterParameter$ SkipTest�� �� Mode$ TwoWay=�	� 5     $��Continue��6"    65   �� $ OnFailerAction�# [ $ Continue��$ TwoWay=�	� 5     $
��Stop��6"    61   �� $ OnFailerAction�# [ $
 Stop��$ TwoWay=�5       5     ���	#FFBED0E7$��Report��68   .@�5       -   5          Name$ 	reportBtn��5  &   $ Images\Chart-1-icon.png��5
  &   5       5     ���	#FFBED0E7$��About��68   .@�5       -   5        $ aboutBtn��5  &   $ Images\Help-icon.png��5  &   5     5       5     $��Settings��6   .@�5       ���	#FFBED0E7.@�5       -   5        $ 
settingBtn��5  &   $��OpenAppConfig��5  &   $ Application��5  &   $3 -Images\Categories-preferences-system-icon.png��5  &   ��5  &   �� *SetToolBarAction+5  &   5       5     ���	#FFBED0E7$
��Font��68   .@�5     �� $ 
Horizontal=�6!   .+�5     �� $	��100��6#   ��5  #   ��  
  System.Windows.Media.Fonts 
  SystemFontFamilies  �� Source#  Z ! �� Text! 5  #   �� *
FontFamily+��5     �� 5   "   .R�5!  &   �� ��60   �� *.+��6I   �� *Source+5"  #   5$     �� 5&     $��40��6#   " �� 
IsEditable" .  {�. ! 5)  #   �� *
FontSize+.@�5*     �� .��6+   116/   �� 5+     .��6+   126/   �� 5,     .��6+   146/   �� 5-     .��6+   166/   �� 5.     .��6+   186/   5/     50     51     52     53     � 54  
   $��1��6   $��Stretch=�55     $��Stretch=�56     # � ColumnDefinitions# 57     �� 58     $
��200* �6#   �� 59     $��4* �6#   �� 5:     $
��600* �6#   .+�5<     �� $��1��6   $��3��5=     $��Center=�5>     $��Stretch=�5?     
������ $ �� ResizeDirection$$ Columns=�5A     )  #Xceed.Wpf.AvalonDock.DockingManager  5C     -   5        $��myDock��5C  "   $��3��5D  "   %   AllowMixedOrientation% . 
���   �$��1q�5G  "   & J� SnapsToDevicePixels& .  '   DocumentHeaderTemplate' 5J     �� 5K     .R�5L     �� $ 
Horizontal=�6%   .+�5M     �� $��0,0,4,0q�6$   ��65   �� *
IconSource+�� 5N     ��6(   �� *Title+5O     5P     (   Theme( 5S     +  %Xceed.Wpf.AvalonDock.Themes.AeroTheme  5T     )   Layout.) 5W     ,  &Xceed.Wpf.AvalonDock.Layout.LayoutRoot  -   5        _layoutRoot��*   	RootPanel.* 5Y     -  'Xceed.Wpf.AvalonDock.Layout.LayoutPanel  +   Orientation$+ 
Horizontal=�6'   �  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutPanelElement, Xceed.Wpf.AvalonDock, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]],   Children., 5Z     ;  5Xceed.Wpf.AvalonDock.Layout.LayoutAnchorablePaneGroup  �  �Xceed.Wpf.AvalonDock.Layout.LayoutPositionableGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutAnchorablePane, Xceed.Wpf.AvalonDock, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]-   	DockWidth$	- 300 �69   �  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutAnchorablePane, Xceed.Wpf.AvalonDock, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]].   Children.. 5[     6  0Xceed.Wpf.AvalonDock.Layout.LayoutAnchorablePane  �  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutAnchorable, Xceed.Wpf.AvalonDock, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]/   Children./ 5]  "   2  ,Xceed.Wpf.AvalonDock.Layout.LayoutAnchorable  /  )Xceed.Wpf.AvalonDock.Layout.LayoutContent0   Title$0  ��68   1   	ContentId$1  ��5^  8   2   
IconSource$)2 #/Images/Actions-system-run-icon.png��5_  8   3   Content.3 5a  &   �� $��Visible=�63   .��5c  *   �� ��63   �� *SelectedSuiteNameGrp+$ PropertyChanged=�.��5d  .   �� $ Vertical=�69   .+�5e  2   &   System.Windows.Controls.DataGrid  -   5        $��TestListGrid��5e  ;   4   AlternatingRowBackground4 �	#FFEAF0F85   AutoGenerateColumns5 .  $��1q�5h  ;   6   CanUserSortColumns6 .  $��14(�5j  ;   ��5k  ;   �� *SuiteEntitiesList+7 �� SelectedValue7 5m  ;   �� *SelectedSuiteEntity+ 5p  6   *  $System.Windows.Controls.DataGridCell��%E   '        �� 5q  :   "  6@   .�5r  >   -   5        5s  ;   5t  7   8 � ContextMenu8 5u  6   �� 5v  :   .@�5w  >   m� $ UpdateSuite��5x  G   $��Update Suite��5y  G    5w  G   �� *SetToolBarAction+9 m� Icon9 5z  B   �� 5{  F   $��20��6L   $��20��5|  L   $��Images\Save-Script.png��5}  L   5  ?   5�  ;   :   Columns: 5�  6   4  .System.Windows.Controls.DataGridCheckBoxColumn  5�  :   ,  &System.Windows.Controls.DataGridColumn;   Width5  /System.Windows.Controls.DataGridLengthConverter$; 55 6Q   <   Header$< Enable��5�  Q   1  +System.Windows.Controls.DataGridBoundColumn=   Binding= 5�  Q   �� *Enable+$ PropertyChanged=�5�  Q   0  *System.Windows.Controls.DataGridTextColumn  5�  :   $	; 230 6M   $< 	Test Name��5�  M   >   
IsReadOnly> . = 5�  M   �� *Name+$ PropertyChanged=�5�  M     5�  :   $	; 230 6M   $< Params��5�  M   = 5�  M   �� *Params+$ PropertyChanged=�5�  M     5�  :   $	; 250 6M   $< Comment��5�  M   = 5�  M   �� *	Comment+$ PropertyChanged=�5�  M   5�  3   �� 5�  2   $��Stretch=�6;   $��Stretch=�5�  ;   $��13(�5�  ;   $��Suite Description��5�  ;   .��5�  6   � .+�5�  :   �� -   5        $��suiteDescriptionTxb��5�  B   $	��200��5�  B   $��Stretch=�5�  B   $��Stretch=�5�  B   ? �� AcceptsReturn? . @ �� TextWrapping$
@ Wrap=�5�  B   "A �� VerticalScrollBarVisibility$A Visible=�5�  B   ��5�  B   �� *SuiteDescription+$ PropertyChanged=�5�  B   5�  7   5�  3   �� 5�  2   $��Suite TearDown Script��6;   .��5�  6   �� $ 
Horizontal=�6A   .+�5�  :   �� $	��225��6B   $��0,0,3,0q�5�  B   ��5�  B   �� *SuiteTearDownScript+$ PropertyChanged=��� 5�  :   -   5        $��ClearTearSuiteDownBtn��5�  A   $��25��5�  A   $��25��5�  A   .��5�  >   �� $��Images\newPic.png��6D   5�  ;   5�  7   5�  3   5�  /   5�  +   5�  '   5�  #     5�  "   $0  ��68   $1 ToolBox��5�  8   $ 2 /Images/tool-box-icon1.png��5�  8   .3 5�  &   R� -   5        $��
treeView11��5�  /   
�������.@�5�  *   Q� $��Stretch=�67   $��General��5�  7   B Q� 
IsExpandedB . .@�5�  .   �� 
������ $��1��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5�  <   $��/Images/clock-icon.png��5�  <   �� 5�  6   $��Sleep��6@   5�  3   5�  /   �� 5�  .   
������ $��2��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5�  <   $)��#/Images/Win-Command-Prompt-icon.png��5�  <   �� 5�  6   $��Run Program��6@   5�  3   5�  /   �� 5�  .   
������ $��4��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5�  <   $��/Images/FileHash.png��5�  <   �� 5�  6   $��Variables Operations��6@   5�  3   5�  /   �� 5�  .   
������ $��27��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5�  <   $ ��/Images/checklist-icon.png��5�  <   �� 5�  6   $
��List��6@   5�  3   5�  /   �� 5�  .   
������ $��28��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5�  <   $*��$/Images/Dictionary-Mac-Book-icon.png��5�  <   �� 5�  6   $��
Dictionary��6@   5�  3   5�  /   �� 5�  .   
������ $��5��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5   <   $ ��/Images/Text-File-icon.png��5  <   �� 5  6   $��Text Operations��6@   5  3   5  /   �� 5  .   
������ $��9��5  5   ��5  5   �� *SelectToolBoxItem+.��5	  2   �� $ 
Horizontal=�6=   .+�5
  6   �� $��20��6<   $��0,0,4,0q�5  <   $��/Images/Timer-icon.png��5  <   �� 5  6   $��Timers��6@   5  3   5  /   �� 5  .   
������ $��13��5  5   ��5  5   �� *SelectToolBoxItem+.��5  2   �� $ 
Horizontal=�6=   .+�5  6   �� $��20��6<   $��0,0,4,0q�5  <   $ ��/Images/data-redo-icon.png��5  <   �� 5  6   $��Sqlite��6@   5  3   5  /   �� 5  .   
������ $��14��5  5   ��5  5   �� *SelectToolBoxItem+.��5  2   �� $ 
Horizontal=�6=   .+�5   6   �� $��20��6<   $��0,0,4,0q�5!  <   $3��-/Images/Apps-preferences-system-time-icon.png��5"  <   �� 5#  6   $��	Date Time��6@   5$  3   5%  /   �� 5'  .   
������ $��15��5)  5   ��5(  5   �� *SelectToolBoxItem+.��5*  2   �� $ 
Horizontal=�6=   .+�5+  6   �� $��20��6<   $��0,0,4,0q�5,  <   $)��#/Images/Mimetype-vcalendar-icon.png��5-  <   �� 5.  6   $��Tables��6@   5/  3   50  /   �� 5?  .   
������ $��17��5A  5   ��5@  5   �� *SelectToolBoxItem+.��5B  2   �� $ 
Horizontal=�6=   .+�5C  6   �� $��20��6<   $��0,0,4,0q�5D  <   $��/Images/Messages-icon.png��5E  <   �� 5F  6   $��Message Show��6@   5G  3   5H  /   �� 5I  .   
������ $��24��5K  5   ��5J  5   �� *SelectToolBoxItem+.��5L  2   �� $ 
Horizontal=�6=   .+�5M  6   �� $��20��6<   $��0,0,4,0q�5N  <   $��/Images/file-icon.png��5O  <   �� 5P  6   $��File Action��6@   5Q  3   5R  /   �� 5S  .   
������ $��20��5U  5   ��5T  5   �� *SelectToolBoxItem+.��5V  2   �� $ 
Horizontal=�6=   .+�5W  6   �� $��20��6<   $��0,0,4,0q�5X  <   $��/Images/ComplexObject.png��5Y  <   �� 5Z  6   $��Script Action��6@   5[  3   5\  /   �� 5]  .   
������ $��25��5_  5   ��5^  5   �� *SelectToolBoxItem+.��5`  2   �� $ 
Horizontal=�6=   .+�5a  6   �� $��20��6<   $��0,0,4,0q�5b  <   $)��#/Images/Actions-system-run-icon.png��5c  <   �� 5d  6   $��System Action��6@   5e  3   5f  /   �� 5g  .   
������ $��26��5i  5   ��5h  5   �� *SelectToolBoxItem+.��5j  2   �� $ 
Horizontal=�6=   .+�5k  6   �� $��20��6<   $��0,0,4,0q�5l  <   $!��/Images/email-send-icon.png��5m  <   �� 5n  6   $��Email��6@   5o  3   5p  /   5q  +   Q� 5r  *   $��Stretch=�67   $
��Flow��5s  7   B . .@�5u  .   �� 
������ $��6��5w  5   ��5v  5   �� *SelectToolBoxItem+.��5x  2   �� $ 
Horizontal=�6=   .+�5y  6   �� $��20��6<   $��0,0,4,0q�5z  <   $��/Images/Bookmark-icon.png��5{  <   �� 5|  6   $��Lables��6@   5}  3   5~  /   �� 5�  .   
������ $��7��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5�  <   $*��$/Images/Document-Flow-Chart-icon.png��5�  <   �� 5�  6   $��	Condition��6@   5�  3   5�  /   �� 5�  .   
������ $��29��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5�  <   $��/Images/Switch-icon.png��5�  <   �� 5�  6   $��Switch Case��6@   5�  3   5�  /   5�  +   Q� 5�  *   $��Stretch=�67   $��Communication��5�  7   B . .@�5�  .   �� 
������ $��8��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5�  <   $(��"/Images/RS232-Data-Logger_icon.gif��5�  <   �� 5�  6   $��RS232��6@   5�  3   5�  /   �� 5�  .   
������ $��10��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5�  <   $.��(/Images/Apps-utilities-terminal-icon.png��5�  <   �� 5�  6   $	��SSH��6@   5�  3   5�  /   �� 5�  .   
������ $��11��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5�  <   $'��!/Images/Computer-Monitor-icon.png��5�  <   �� 5�  6   $��Telent��6@   5�  3   5�  /   �� 5�  .   
������ $��12��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5�  <   $&�� /Images/Filesystems-ftp-icon.png��5�  <   �� 5�  6   $
��Sftp��6@   5�  3   5�  /   �� 5�  .   
������ $��30��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5�  <   $!��Images\Home-Server-icon.png��5�  <   �� 5�  6   $��Remote Server��6@   5�  3   5�  /   �� 5�  .   
������ $��21��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5�  <   $'��!/Images/Activity-Monitor-icon.png��5�  <   �� 5�  6   $
��Ping��6@   5�  3   5�  /   5�  +   Q� 5�  *   $��Stretch=�67   $��Devices��5�  7   B . .@�5�  .   �� 
������ $��23��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5�  <   $��/Images/motor.png��5�  <   �� 5�  6   $��Motor Controller��6@   5�  3   5�  /   �� 5�  .   
������ $��18��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5�  <   $(��"/Images/Style-Switch-User-icon.png��5�  <   �� 5�  6   $��Relay Control��6@   5�  3   5�  /   �� 5�  .   
������ $��22��5�  5   ��5�  5   �� *SelectToolBoxItem+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��20��6<   $��0,0,4,0q�5�  <   $��/Images/rsz_hr80ps.png��5�  <   �� 5�  6   $��HR80 Power Supply��6@   5�  3   5�  /   5�  +   5�  '   5�  #     5   "   $0  ��68   $1  ��5  8   $2 /Images/Save-Script.png��5  8   .3 5  &   �� $��Visible=�63   .��5  *   �� -   5        $��	stackTree��5  5   .+�5  .   (   "AutomationClient.SerachUserControl  -   5        C  Name$C searchControl��5  F   (   "AutomationClient.SerachUserControlD   OpenSelectedFileClick$)D #searchControl_OpenSelectedFileClick��5  F   E   RunFilterClick$"E searchControl_RunFilterClick��5	  F   $	��150��5  F   5	  F   R� 5  .   -   5        $��ScriptsTreeView��5  7   .@�5  2   Q� $��Scripts��6?   ��6P   �� *ScannerScriptFiles+��5  6   �� 5  :   F �� ItemsSourceF 6S   �� $ Files�.R�5  >   �� $ 
Horizontal=�6I   .+�5  B   �� $��0,0,2,0q�6I   
�������
������ ��5  I   �� G �� ElementName$G ScriptsTreeView��$ DataContext.NodeAddToList���5  I   �� *
FullPath+.��5  F   �� $ 
Horizontal=�6Q   .+�5  J   �� $��20��6P   ��6\   �� *ActionImage+5  G   5  C   �� 5  B   
�������
������ ��5  I   �� $G ScriptsTreeView��$ DataContext.NodeSelection���5   I   �� *
FullPath+.��5"  F   �� $ 
Horizontal=�6Q   .+�5#  J   �� $��20��6P   $��0,0,3,0q�5$  P   ��5%  P   �� *Image+�� 5&  J   $��12(�6T   ��6b   �� *Label+8 5(  N   �� 5)  R   .@�5*  V   m� $��MenuItem��6_   -   5        $��Add as step��5,  _   
H � TagH 5-  _   �� $ FullPath�9 5.  Z   �� 5/  ^   $��20��6d   $��20��50  d   $��/Images/math-add-icon.png��51  d   53  W   m� 55  V   $��MenuItemTearDown��6_   -   5        $��Add as Suite TearDown��57  _   H 58  _   �� $ FullPath�9 59  Z   �� 5:  ^   $��20��6d   $��20��5;  d   $��/Images/math-add-icon.png��5<  d   5>  W   5@  S   5B  K   5C  G   5D  C   5E  ?   5F  ;   5H  3   Q� 5I  2   $��
Test Cases��6?   ��6S   �� *ScannerTestFiles+��5J  6   �� 5K  :   F 6S   �� $ Files�.R�5L  >   �� $ 
Horizontal=�6I   .+�5N  B   �� $��0,0,2,0q�6I   
�������
������ ��5Q  I   �� $G ScriptsTreeView��$ DataContext.NodeAddToList���5S  I   �� *
FullPath+.��5T  F   �� $ 
Horizontal=�6Q   .+�5U  J   �� $��20��6P   ��6\   �� *ActionImage+5V  G   5W  C   �� 5Y  B   
�������
������ ��5[  I   �� $G ScriptsTreeView��$ DataContext.NodeSelection���5]  I   �� *
FullPath+.��5^  F   �� $ 
Horizontal=�6Q   .+�5_  J   �� $��20��6P   $��0,0,3,0q�5`  P   ��5a  P   �� *Image+�� 5b  J   $��12(�6T   ��6b   �� *Label+5c  G   5d  C   5e  ?   5f  ;   5h  3   Q� 5i  2   $��Test Suites��6?   ��6T   �� *ScannerSuitesFiles+��5j  6   �� 5k  :   F 6S   �� $ Files�.R�5l  >   �� $ 
Horizontal=�6I   .+�5n  B   �� $��0,0,2,0q�6I   
�������
������ ��5q  I   �� $G ScriptsTreeView��$ DataContext.NodeAddToList���5s  I   �� *
FullPath+.��5t  F   �� $ 
Horizontal=�6Q   .+�5u  J   �� $��20��6P   ��6\   �� *ActionImage+5v  G   5w  C   �� 5y  B   
�������
������ ��5{  I   �� $G ScriptsTreeView��$ DataContext.NodeSelection���5}  I   �� *
FullPath+.��5~  F   �� $ 
Horizontal=�6Q   .+�5  J   �� $��20��6P   $��0,0,3,0q�5�  P   ��5�  P   �� *Image+�� 5�  J   $��12(�6T   ��6b   �� *Label+5�  G   5�  C   5�  ?   5�  ;   5�  3   5�  /   5�  +   5�  '   5�  #   5�     5�     9   3Xceed.Wpf.AvalonDock.Layout.LayoutDocumentPaneGroup   5�     �!  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutDocumentPane, Xceed.Wpf.AvalonDock, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]I !  Children.I 5�     4"  .Xceed.Wpf.AvalonDock.Layout.LayoutDocumentPane"  �#  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutContent, Xceed.Wpf.AvalonDock, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]J #  Children.J 5�  "   0$  *Xceed.Wpf.AvalonDock.Layout.LayoutDocument$  $
0 Edit��66   $
1 Edit��5�  6   $'2 !/Images/Apps-text-editor-icon.png��5�  6   .3 5�  &   � $��Stretch=�6+   $��Stretch=�6I   # 5�  *   �� 5�  .   $
��500* �6?   �� 5�  .   $��4* �6?   �� 5�  .   $
��500* �6?   .+�5�  *   � $��0��6/    5�  .   �� 5�  2   $
��535* �6@   �� 5�  2   $��4* �6@   �� 5�  2   $
��122* �6@   .+�5�  .   �� -   5        $��scriptGroupBox��5�  7   $��Stretch=�5�  7   $��Stretch=�5�  7   ��5�  7   �� *SelectedScriptNameGrp+$ PropertyChanged=�.��5�  2     -   5        $��dgStepEntities��5�  ;   5 .  $��1q�5�  ;   K   CanUserReorderColumnsK . L   CanUserResizeColumnsL . M   CanUserResizeRowsM .  6 .  $��14(�5�  ;   ��5�  ;   �� *StepEntitiesList+$ PropertyChanged=�7 5�  ;   �� *SelectedStepEntity+8 5�  6   �� 5�  :   .@�5�  >   m� $ UpdateScript��5�  G   $��Save Script��5�  G    5�  G   �� *SetToolBarAction+9 5�  B   �� 5�  F   $��20��6L   $��20��5�  L   $��Images\Save-Script.png��5�  L   5�  ?   m� 5�  >   $ InvertselectSteps��5�  G   $��Invert select��5�  G    5�  G   �� *SetToolBarAction+9 5�  B   �� 5�  F   $��20��6L   $��20��5�  L   $��Images\refresh_switch.png��5�  L   5�  ?   m� 5�  >   $ SelectAllSteps��5�  G   $��
Select All��5�  G    5�  G   �� *SetToolBarAction+9 5�  B   �� 5�  F   $��20��6L   $��20��5�  L   $��Images\Check-icon.png��5�  L   5�  ?   m� 5�  >   $ 	NewScript��5�  G   $��
New Script��5�  G    5�  G   �� *SetToolBarAction+9 5�  B   �� 5�  F   $��20��6L   $��20��5�  L   $��Images\newPic.png��5�  L   5�  ?   m� 5�  >   $ 	AddToTest��5�  G   $��Add To Test��5�  G    5�  G   �� *SetToolBarAction+9 5�  B   �� 5�  F   $��20��6L   $��20��5�  L   $��Images\Plus-icon.png��5�  L   5�  ?   5�  ;   ��5�  6   �� 5�  :   "  6@   .�5�  >   ��  
 �@ AlternatingRowBackground 6i   �� *AlternatingRowBackground+5�  ;   N �� 
GroupStyleN 5�  6   (%  "System.Windows.Controls.GroupStyle%  5�  :   O %  HeaderTemplateO 5�  >   �� 5�  B   .R�5�  F   �� .+�5�  J   �� ��6T   �� $
 Name�5�  G   5�  C   P %  ContainerStyleP 5�  >   �� 5�  B   " ��6H   .�5�  F   �� 
 � �� 5�  J   �� 5�  N   Q �� 
TargetType"Q ��6^   .R�5   R   9� ��5  V   �� 5  Z   $ 
Horizontal=�6e   .+�5  ^   �� ��6h   �� $
 Name��� 5  ^   ��6h   �� $ 	ItemCount��� 5  ^   $��Items��6h   5  [   .��5  V   �� 5	  S   5
  O   5  G   5  C   5  ;   ��5  6   �� 5  :   )&  #System.Windows.Controls.DataGridRow" & 6@   .�5  >   -    5        5  ;    5  6   ��%E   '        �� 5  :   "  6@   .�5  >   -!   5        5  ;   5&  7   : 5(  6     5*  :   $; 55 6Q   $< Enable��5-  Q   = 5+  Q   �� *Enable+$ PropertyChanged=�5-  Q     5.  :   $	; 150 6M   $< 	Type Name��51  M   > . = 5/  M   �� *Name+$ PropertyChanged=�52  M     53  :   $	; 250 6M   $< Comment��56  M   = 54  M   �� *	Comment+$ PropertyChanged=�56  M     57  :   $	; 150 6M   $< Step Details��5:  M   > . = 58  M   �� *StepDetails+$ PropertyChanged=�5;  M     5<  :   $	; 120 6M   $< On Failure Lable��5?  M   = 5=  M   �� *OnFailureLabel+$ PropertyChanged=�5?  M   5A  3   5B  /   �� 5C  .   $��1��6;   $��2��5D  ;   $��3��5E  ;   $��Stretch=�5F  ;   $��Center=�5G  ;   
������ $
$ Rows=�5I  ;   �� 5J  .   $��2��67   $��Stretch=�5K  7   $��Stretch=�5L  7   $��13(�5M  7   $��Script Description��5N  7   .��5O  2   � .+�5Q  6   �� -"   5        $��scriptDescriptionTxb��5Q  >   $��Stretch=�5R  >   $��Stretch=�5S  >   ? . $
@ Wrap=�5W  >   $A Visible=�5X  >   ��5U  >   �� *ScriptDescription+$ PropertyChanged=�5X  >   5Y  3   5Z  /   5[  +   �� 5\  *   $��1��67   $��3��5]  7   $��Center=�5^  7   $��Stretch=�5_  7   
������ $$ Columns=�5a  7   � 5b  *   $��2��6/    5c  .   �� 5d  2   $
��535* �6@   �� 5e  2   $��4* �6@   �� 5f  2   $
��122* �6@   .+�5h  .   �� $��1��6;   $��2��5i  ;   $��3��5j  ;   $��Stretch=�5k  ;   $��Center=�5l  ;   
������ $
$ Rows=�5n  ;   �� 5o  .   $��2��67   $��Stretch=�5p  7   $��Stretch=�5q  7   $��13(�5r  7   $��Test Description��5s  7   .��5t  2   � .+�5u  6   �� -#   5        $��testDescriptionTxb��5u  >   $��Stretch=�5v  >   $��Stretch=�5w  >   ? . $
@ Wrap=�5{  >   $A Visible=�5|  >   ��5y  >   �� *TestDescription+$ PropertyChanged=�5|  >   5}  3   5~  /   �� 5  .   $��Stretch=�67   $��Stretch=�5�  7   ��5�  7   �� *SelectedTestNameGrp+$ PropertyChanged=�.��5�  2     -$   5        $��testDataGrid��5�  ;   4 �	#FFEAF0F85 .  $��1q�5�  ;   6 .  $��14(�5�  ;   ��5�  ;   �� *TestEntitiesList+R �� SelectedIndexR 5�  ;   �� *SelectedTestEntitiesIndex+7 5�  ;   �� *SelectedTestEntity+8 5�  6   �� 5�  :   .@�5�  >   m� $ 
UpdateTest��5�  G   $��	Save Test��5�  G    5�  G   �� *SetToolBarAction+9 5�  B   �� 5�  F   $��20��6L   $��20��5�  L   $��Images\Save-Script.png��5�  L   5�  ?   m� 5�  >   $ InvertselectScripts��5�  G   $��Invert select��5�  G    5�  G   �� *SetToolBarAction+9 5�  B   �� 5�  F   $��20��6L   $��20��5�  L   $��Images\refresh_switch.png��5�  L   5�  ?   m� 5�  >   $ SelectAllScripts��5�  G   $��
Select All��5�  G    5�  G   �� *SetToolBarAction+9 5�  B   �� 5�  F   $��20��6L   $��20��5�  L   $��Images\Check-icon.png��5�  L   5�  ?   m� 5�  >   $ 
AddToSuite��5�  G   $��Add To Suite��5�  G    5�  G   �� *SetToolBarAction+9 5�  B   �� 5�  F   $��20��6L   $��20��5�  L   $��Images\Plus-icon.png��5�  L   5�  ?   5�  ;   ��5�  6   �� 5�  :   " & 6@   .�5�  >   �� 
 �@J�	AllowDrop . 6Z   5�  ;    5�  6   ��%E   '        �� 5�  :   "  6@   .�5�  >   -%   5        5�  ;   5�  7   : 5�  6     5�  :   $; 55 6Q   $< Enable��5�  Q   = 5�  Q   �� *Enable+$ PropertyChanged=�5�  Q     5�  :   $	; 150 6M   $< Script Name��5�  M   > . = 5�  M   �� *Name+$ PropertyChanged=�5�  M     5�  :   $	; 250 6M   $< Params��5�  M   = 5�  M   �� *Params+$ PropertyChanged=�5�  M     5�  :   $	; 250 6M   $< Comment��5�  M   = 5�  M   �� *	Comment+$ PropertyChanged=�5�  M   5�  3   5�  /   5�  +   5�  '   5�  #   $  5�  "   $0 Execution Status��66   $1 ExecutionStatus��5�  6   $)2 #/Images/Actions-system-run-icon.png��5�  6   .3 5�  &   �  5�  *   �� 5�  .   $��42 �6<   �� 5�  .   $��* �6<   .+�5�  *   �� $��42��65   $��0,2,0,0q�6A   .+�5�  .   � # 5�  2   �� 5�  6    5�  2   �� 5�  6   �� 5�  6   .+�5�  2   �� $��0��6=   $��5,0,0,0q�5�  =   $ 
Horizontal=�5�  =   .+�5�  6   �� $��0,0,6,0q�6@   $��Passed Cycles:��6Q   �� 5�  6   $��50��6>   $��20��5�  >   S �� 
IsReadOnlyS . ��5�  >   �� $ ActivatedCycles�$ OneWay=�$ PropertyChanged=��� 5�  6   $��5,0,3,0q�6@   $��Active Suite Name:��6Q   �� 5�  6   $	��400��6>   $��20��5�  >   $��0,0,7,0q�5�  >   S . ��5�  >   �� $ ActiveSuiteName�$ OneWay=�$ PropertyChanged=��� 5   6   -&   5        $��autoScrollChk��5   ?   $��Auto Scroll��5  ?   $
 TrueY�5  ?   5  3   �� 5  2   $��1��6=   $��5,0,0,0q�5  =   $ 
Horizontal=�5  =   .+�5	  6   �� $��0,0,3,0q�6@   $��Suite Progress:��6Q   �� 5
  6   $��50��6>   $��20��5  >   S . ��5  >   �� $ 
Percentage�$ OneWay=�$ PropertyChanged=��� 5  6   $��5,0,8,0q�6@   $��Active Test Name:��6Q   �� 5  6   $	��400��6>   $��20��5  >   S . ��5  >   �� $ ActiveTestName�$ OneWay=�$ PropertyChanged=��� 5  6   $��5,0,8,0q�6@   $��Active Step Info:��6Q   �� 5  6   $	��200��6>   $��20��5  >   S . ��5  >   �� $ ActiveStepInfo�$ OneWay=�$ PropertyChanged=�5  3   5   /   5!  +     5#  *   -'   5        $��executionDatagrid��5#  3   $��1��5$  3   $��0,5,0,5q�5%  3   4 �	#FFEAF0F85 .  $��1q�5(  3   6 .  $��14(�5*  3   ��5+  3   �� *ClientMessages+$ PropertyChanged=�('   "AutomationClient.DesignerViewModelT '  DataGridT 5-  3   �� $G executionDatagrid��: 5.  .     5/  2   $; 50 6E   $< Index��52  E   > . = 50  E   �� *Index+$ PropertyChanged=�53  E     54  2   $	; 200 6E   $
< Time��57  E   > . = 55  E   �� *Time+$ PropertyChanged=�58  E     59  2   $	; 730 6E   $
< Info��5<  E   > . = 5:  E   �� *Info+$ PropertyChanged=�5=  E   4(  .System.Windows.Controls.DataGridTemplateColumn(  5@  2   $< Status��6I   U (  CellTemplateU 5A  6   �� 5B  :   .R�5C  >   �� $��statusImage��6D   $��18��5D  D   $��18��5E  D    5F  >   �� 5G  B   $
 Pass��6i    6N   �� *Status+.a�5H  F   �� $	 statusImage��6M   
 � ��$ Images\passPic.png��6x   5I  C   �� 5J  B   $
 Fail��6i    6N   �� *Status+.a�5K  F   �� $	 statusImage��6M   
 � ��$ Images\failPic.png��6x   5L  C   �� 5M  B   $
 Info��6i    6N   �� *Status+.a�5N  F   �� $	 statusImage��6M   
 � ��$ Images\infoPic.png��6x   5O  C   �� 5P  B   $ Skipped��6i    6N   �� *Status+.a�5Q  F   �� $	 statusImage��6M   
 � ��$ Images\skipPic.png��6x   5R  C   �� 5S  B   $ 	Exception��6i    6N   �� *Status+.a�5T  F   �� $	 statusImage��6M   
 � ��$ Images\Hazard.png��6x   5U  C   5W  ;   5Y  3   5[  +   5\  '   5]  #   $  5_  "   $0 	Variables��66   $1 	Variables��5`  6   $2 /Images/Hash32.jpg��5a  6   .3 5b  &   �  5c  *   �� 5d  .   $��30 �6<   �� 5e  .   $	��478 �6<   �� 5f  .   $��8 �6<   �� 5g  .   $��* �6<   .+�5i  *   c� $��0��62   .@�5j  .   �� $��30��65   $��30��5k  5   $��
GetVarList��5m  5   ��5l  5   �� *SetToolBarAction+.��5n  2   �� $ ��Images\Sync-Green-icon.png��68   5o  /   5p  +     5r  *   -(   5        $��variablesDataGrid��5r  3   $��1��5s  3   $��0,9,0,1q�5t  3   4 �	#FFEAF0F85 .  $��1q�5w  3   K . L . M .  6 . $��14(�5|  3   ��5}  3   �� *VariableList+7 5  3   �� *SelectedVariable+: 5�  .     5�  2   $	; 300 6E   $< Variable Name��5�  E   > . = 5�  E   �� *.+$ PropertyChanged=�5�  E   5�  +   �� 5�  *   $��2��67   $��1��5�  7   $��Stretch=�5�  7   $��Stretch=�5�  7   
������ $
$ Rows=�5�  7   �� 5�  *   -)   5        $��
varDataTxb��5�  2   $��3��5�  2   $V �� HorizontalScrollBarVisibility$
V Auto=�5�  2   $@ WrapWithOverflow=�5�  2   $
A Auto=�5�  2   5�  '   5�  #   5�     5�     5�     5�     5�     5�     �� 5�  
   -*   5        $��
statusBar1��5�     $��2��5�     $��Stretch=�5�     $��Stretch=�5�     .@�5�     �� $��20��6   ��6    �� *ConnectionIcon+$ PropertyChanged=��� 5�     $��Normal$�6   $
��Bold"�5�     ���	#FFD11313��5�     �� *MessageInfo+5�     5�     5�     