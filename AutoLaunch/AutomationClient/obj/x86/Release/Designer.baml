   M S B A M L   `   `   `  ���� K  GAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:AutomationClientAutomationClient      AutomationClient.Designer   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     0localclr-namespace:AutomationClient     Imy@http://schemas.microsoft.com/winfx/2006/xaml/presentation/ribbon    � Icon$>  8Images\Apps-preferences-desktop-launch-feedback-icon.png�� � Title$ Auto Launch Client��$	��849��$
��1364�� � WindowState$ 	Maximized=� � 	Resources .   (AutomationClient.RadioBoolToIntConverter   radioBoolToIntConverter  MessageResultsImages ��Name �� Triggers �� Value �� Binding �� Path	 �� 
TargetName
 �� Property �� Value��%V  &	        &	        �� .R��� $ StatusImage�� �� $ 0�� �� $ Status�.a��� $	 StatusImage��
 � ��$ Images\passPic.png���� $ 1�� �� $ Status�.a��� $	 StatusImage��
 � ��$ Images\failPic.png���� $ 2�� �� $ Status�.a��� $	 StatusImage��
 � ��$ Images\infoPic.png��.���  � RowDefinitions �� $
��Auto ��� $
��642* ��� $	��25* �.+�.  (Microsoft.Windows.Controls.Ribbon.Ribbon  ���	#FF749CCD$��Stretch=�$��Stretch=�   ApplicationMenu =  7Microsoft.Windows.Controls.Ribbon.RibbonApplicationMenu  .@�A  ;Microsoft.Windows.Controls.Ribbon.RibbonApplicationMenuItem  -     Name$ saveScriptBtn��$��Save Script��6  0Microsoft.Windows.Controls.Ribbon.RibbonMenuItem   ImageSource$ Images\Save-Script.png�� m� CommandParameter$ 
SaveScript�� m� Command �� *SetToolBarAction+  $��	Save Test��$ Images\Save-Test.png��$ SaveTest�� �� *SetToolBarAction+  $��
Save Suite��$ Images\Save-Test.png��$ 	SaveSuite�� �� *SetToolBarAction+  -   $ exitMenuBtn��$
��Exit��$* $Images\Actions-dialog-close-icon.png��  $	��100��{�.  .@�1  +Microsoft.Windows.Controls.Ribbon.RibbonTab  -   $��HomeTab��$
��Home��$��75��.@�3  -Microsoft.Windows.Controls.Ribbon.RibbonGroup  ���	#FFBED0E7$
��Edit��.@�4  .Microsoft.Windows.Controls.Ribbon.RibbonButton  -   $��Clear��$��ClearAllEntities��   Label$ Clear��   LargeImageSource$ Images\newPic.png������ *SetToolBarAction+  -   $��	Duplicate��$��DuplicateStepEntity��$ 	Duplicate��$ Images\paste-icon.png������ *SetToolBarAction+����  �� Triggers ��  �� *SelectedStepEntity+ Z� .a��� 
 � {� .    -   $��moveUp��$��MoveElementUp��$ Up��$ Images\Up-32.png������ *SetToolBarAction+  -   $��moveDw��$��MoveElementDown��$
 Down��$ Images\down-32.png������ *SetToolBarAction+  ���	#FFBED0E7$��Server��.@�  $ Connect��$! Images\Home-Server-icon.png��$��ServerConnect������ *SetToolBarAction+  ���	#FFBED0E7$��Execute��.@�  $ Pause��$ Images\pause-icon.png��$��PauseExecution������ *SetToolBarAction+����  ��  �� *ServerConnect+ Z� .a��� 
 � {� .    $
 Stop��$ Images\stop-icon.png��$��StopExecution������ *SetToolBarAction+����  ��  �� *ServerConnect+ Z� .a��� 
 � {� .    -   $��runBtn��$	 Run��$ Images\play-icon.png��$��ExecuteTest������ *SetToolBarAction+����  ��  �� *ServerConnect+ Z� .a��� 
 � {� .  ��  �� *SelectedSuiteName+ Z� .a��� 
 � {� .  ��  �� Orientation$ 
Horizontal=�.+��� $��Cycles:    ���� $��60��$��20������ $ Cycles� �� UpdateSourceTrigger$ PropertyChanged=�  $��
On Failure�����	#FFBED0E7.@�	� $��	Skip Test�� d� 	IsChecked �� $ OnFailerAction� �� 	Converter# [   �� ConverterParameter$ SkipTest�� �� Mode$ TwoWay=�	� $��Continue�� �� $ OnFailerAction�# [  $ Continue��$ TwoWay=�	� $
��Stop�� �� $ OnFailerAction�# [  $
 Stop��$ TwoWay=�  $��About�����	#FFBED0E7.@�  -	     Name$ aboutBtn��$ Images\Help-icon.png��  $��Settings��� $��1��$��Stretch=�$��Stretch=� � ColumnDefinitions �� $
��200* ��� $��4* ��� $
��600* �.+���  �� ResizeDirection$ Columns=�
������ $��Stretch=�$��3��$��Center=�$��1��� $��Stretch=�$��Stretch=� �� $
��300* ��� $��4* ��� $
��122* �.+��� $
 Rows=�
������ $��Center=�$��Stretch=�$��1��$��3��$��2���� $��Stretch=�$��Stretch=�$��13(�$��2��$��2��.@��� $��Saved Items��.��R� -
   $��ScriptsTreeView�����	#FFF0F0F0.@�Q� $��Scripts������ *ScannerScriptFiles+����  �� ItemsSource �� $ Files�.R��� $ 
Horizontal=�.+��� 
������ 
�������$��0,0,2,0q�����  �� ElementName$ ScriptsTreeView��$ DataContext.NodeAddToList����� *
FullPath+.���� $ 
Horizontal=�.+��� $��20������ *ActionImage+�� 
������ 
����������� $ ScriptsTreeView��$ DataContext.NodeSelection����� *
FullPath+.���� $ 
Horizontal=�.+��� $��20��$��0,0,3,0q����� *Image+�� $��12(����� *Label+Q� $��Tests������ *ScannerTestFiles+����  �� $ Files�.R��� $ 
Horizontal=�.+��� 
������ 
�������$��0,0,2,0q����� $ ScriptsTreeView��$ DataContext.NodeAddToList����� *
FullPath+.���� $ 
Horizontal=�.+��� $��20������ *ActionImage+�� 
������ 
����������� $ ScriptsTreeView��$ DataContext.NodeSelection����� *
FullPath+.���� $ 
Horizontal=�.+��� $��20��$��0,0,3,0q����� *Image+�� $��12(����� *Label+Q� $��Suites������ *ScannerSuitesFiles+����  �� $ Files�.R��� $ 
Horizontal=�.+��� 
������ 
�������$��0,0,2,0q����� $ ScriptsTreeView��$ DataContext.NodeAddToList����� *
FullPath+.���� $ 
Horizontal=�.+��� $��20������ *ActionImage+�� 
������ 
����������� $ ScriptsTreeView��$ DataContext.NodeSelection����� *
FullPath+.���� $ 
Horizontal=�.+��� $��20��$��0,0,3,0q����� *Image+�� $��12(����� *Label+�� $��0��.@��� $��ToolBox��.���� $��Stretch=�$��Stretch=�.��R� -   $��	treeView1�����	#FFF0F0F0.@�Q� $��	ClipBoard��  Q� 
IsExpanded  . $��Stretch=�.@��� $ 
Horizontal=�.+��� $ ��/Images/Clipboard-icon.png��$��20���� 
������ $��Get Text��
�������$��2������ *SelectToolBoxItem+�� $ 
Horizontal=�.+��� $&�� /Images/Clipboard-Paste-icon.png��$��20���� 
������ $��
Paste Text��
�������$��2������ *SelectToolBoxItem+�� $ 
Horizontal=�.+��� $$��/Images/Text-Document-icon.png��$��20���� 
������ $��Clear��
�������$��2������ *SelectToolBoxItem+Q� $��General��  . $��Stretch=�.@��� $ 
Horizontal=�.+��� $��/Images/clock-icon.png��$��20���� 
������ $��Sleep�����	#FFDBDBDB$��1������ *SelectToolBoxItem+�� $ 
Horizontal=�.+��� $)��#/Images/Win-Command-Prompt-icon.png��$��20���� 
������ $��Run Program�����	#FFDBDBDB$��2������ *SelectToolBoxItem+�� $ 
Horizontal=�.+��� $$��/Images/numeric_field_icon.jpg��$��20���� 
������ $��Variables Operations�����	#FFDBDBDB$��4������ *SelectToolBoxItem+�� $��	Execution��.���� .��&	   System.Windows.Controls.DataGrid	  -   ! 	  AlternatingRowBackground! �	#FFDADFE9" 	  AutoGenerateColumns" .  $��1q�$��14(����� *SuiteEntitiesList+# �� SelectedValue# �� *SelectedSuiteEntity+$ 	  Columns$ 4
  .System.Windows.Controls.DataGridCheckBoxColumn
  ,  &System.Windows.Controls.DataGridColumn%   Header$% Enable��&   Width5  /System.Windows.Controls.DataGridLengthConverter$& 55 1  +System.Windows.Controls.DataGridBoundColumn'   Binding' �� *Enable+$ PropertyChanged=�0  *System.Windows.Controls.DataGridTextColumn  $% 	Test Name��(   
IsReadOnly( . $	& 230 ' �� *Name+$ PropertyChanged=��� -   $��MainTabControl��$��2��.@��� $
��Edit��.��� $��Stretch=�$��Stretch=� �� $
��500* ��� $��4* ��� $
��500* �.+�� $��0�� �� $
��535* ��� $��4* ��� $
��122* �.+��� -   $��scriptGroupBox��$��Stretch=�$��Script��$��Stretch=�.��	  -   $��dgStepEntities��! �	#FFDADFE9" .  $��1q�$��14(����� *StepEntitiesList+# �� *SelectedStepEntity+$ 
  $% Enable��$& 55 ' �� *Enable+$ PropertyChanged=�  $% 	Type Name��( . $	& 150 ' �� *Name+$ PropertyChanged=�  $% Comment��$	& 250 ' �� *	Comment+$ PropertyChanged=��� $
 Rows=�
������ $��Center=�$��Stretch=�$��1��$��3��$��2���� $��Script Description��$��Stretch=�$��Stretch=�$��13(�$��2��.��� .+��� -   $��scriptDescriptionTxb��$��Stretch=�$��Stretch=�) �� TextWrapping$
) Wrap=�"* �� VerticalScrollBarVisibility$* Visible=�+ �� AcceptsReturn+ . ���� *ScriptDescription+$ PropertyChanged=��� $��1��$ Columns=�
������ $��Stretch=�$��3��$��Center=�� $��2�� �� $
��535* ��� $��4* ��� $
��122* �.+��� $
 Rows=�
������ $��Center=�$��Stretch=�$��1��$��3��$��2���� $��Test Description��$��Stretch=�$��Stretch=�$��13(�$��2��.��� .+��� -   $��testDescriptionTxb��$��Stretch=�$��Stretch=�$
) Wrap=�$* Visible=�+ . ���� *TestDescription+$ PropertyChanged=��� $��Stretch=�$
��Test��$��Stretch=�.��	  -   ! �	#FFDADFE9" .  $��1q�$��14(����� *TestEntitiesList+# �� *SelectedTestEntity+$ 
  $% Enable��$& 55 ' �� *Enable+$ PropertyChanged=�  $% Script Name��( . $	& 150 ' �� *Name+$ PropertyChanged=�  $% Comment��$	& 250 ' �� *	Comment+$ PropertyChanged=��� $��Execution Status��.���  �� $��42 ��� $��* �.+��� $��42��$��0,2,0,0q�.+��� $ 
Horizontal=�$��5,0,0,0q�.+��� $��Passed Cycles:��$��0,0,6,0q��� , �� 
IsReadOnly, . $��50��$��20������ $ ActivatedCycles�$ OneWay=�$ PropertyChanged=��� $��Active Suite Name:��$��5,0,3,0q��� , . $	��200��$��20������ $ ActiveSuiteName�$ OneWay=�$ PropertyChanged=��� $ 
Horizontal=�$��5,0,0,0q�.+��� $��Suite Progress:��$��0,0,3,0q��� , . $��50��$��20������ $ 
Percentage�$ OneWay=�$ PropertyChanged=��� $��Active Test Name:��$��5,0,8,0q��� , . $	��200��$��20������ $ ActiveTestName�$ OneWay=�$ PropertyChanged=�	  $��1��! �	#FFDADFE9" .  $��1q�$��14(�$��0,0,0,5q����� *ClientMessages+$   $% Index��( . $& 50 ' �� *Index+$ PropertyChanged=�  $
% Time��( . $	& 200 ' �� *Time+$ PropertyChanged=�  $
% Info��( . $	& 730 ' �� *Info+$ PropertyChanged=�4  .System.Windows.Controls.DataGridTemplateColumn  $% Status��-   CellTemplate- �� .R��� $��statusImage��$��18��$��18�� �� $
 Pass�� �� *Status+.a��� $	 statusImage��
 � ��$ Images\passPic.png���� $
 Fail�� �� *Status+.a��� $	 statusImage��
 � ��$ Images\failPic.png���� $
 Info�� �� *Status+.a��� $	 statusImage��
 � ��$ Images\infoPic.png���� $ Skipped�� �� *Status+.a��� $	 statusImage��
 � ��$ Images\skipPic.png���� $ 	Exception�� �� *Status+.a��� $	 statusImage��
 � ��$ Images\Hazard.png���� -   $��
statusBar1��$��2��$��Stretch=�$��Stretch=�