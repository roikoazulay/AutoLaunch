   M S B A M L   `   `   `  ����  AutomationClient    AutomationClient.AboutWin   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Icon$>  8Images\Apps-preferences-desktop-launch-feedback-icon.png��5   	      AboutDialogStyle#���   5   	    � WindowStartupLocation$ CenterOwner=�5	   	   
idAboutBox�� � Title 5   	   ��  �� RelativeSource �� *Self+ �� Mode$ OneTime=� �� Path$ ProductTitle� � 	Resources 5        	aboutLogo �� 
TargetType �� Property	 �� Value  DisplayAreaStyle  BackgroundStyle
 �� EndPoint � Offset � Color  OkButtonStyle��%�  &	       &	  R     &	 �    &	 �    '�� #    &	 j    �� 5   
   $>��8Images\Apps-preferences-desktop-launch-feedback-icon.png��6"   �� 5   
   " �6)   .�5      ��  � ��$
	 Auto��6'   �� 5       � ��$		 300��6&   �� 5       �@�ShowInTaskbar	 .  6.   �� 5       �@�
ResizeMode$	 NoResize=�6+   �� 5       �@�WindowStyle$	 SingleBorderWindow=�6,   �� 5       �@�SizeToContent$	 Height=�6.   5      �� 5   
   " ��6)   .�5      ��  � ��$	 
10,10,10,5q�6'   5      �� 5   
   " ��6(   .�5      ��  � ��	 5      �� 5      $	
 0,1&�6*   .��5      � $ 0Q�6'   $ 	LightBlue��62   � 5      $
 0.55Q�6'   $ White��65   � 5      $
 0.85Q�6'   $ White��65   � 5      $ 1Q�6'   $ 	LightBlue��62   5       5"      5#      �� 5$   
   " ��6   .�5%      ��  � ��$	 0q�6(   5&      �� 5'   
   " ��6&   .�5(      ��  � ��$	 75��6&   �� 5)       � ��$	 0q�6'   �� 5*       � ��$
	 Left=�6/   �� 5+       �@��	IsDefault	 . 6*   �� 5,       �@��IsCancel	 . 6)   5-      5.      .��5/      �� #��[ 6   
clientArea��.+�50   
   ��  �� ResourceKey$ 	aboutLogo��6   �� 51   
   #��[ 54      displayArea�� � DataContext 51      ��  �� *FindAncestor+ �� AncestorType" �$ OneTime=�.+�56      �� -   5         ��Name$ productName��56      productName����57      �� $ OneTime=�$ Product�59      �� 5:       �� Orientation$ 
Horizontal=�6   versionArea��.+�5;      �� -   5        $ versionLabel��5;      $��
Version - ��5<      VersionLabel��5=      �� 5>      -   5        $ version��5>      version����5?      �� $ OneTime=�$ Version�5A      5B      �� 5C      -   5        $ 	copyright��5C      	copyright����5D      �� $ OneTime=�$ 	Copyright�5F      �� 5H      -   5        $ reserved��5H      $��All Rights Reserved.��5I      reserved��5J      ]� 5K      
buttonArea��.+�5L      �� -   5         ��Name$ okButton��5L      $��OK��5N      #��[ 5O      okButton��5P      5Q      5R      5S      5T      