   M S B A M L   `   `   `  ����G  CTask Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:Task_Software.SideWindowCollection"Task_Software.SideWindowCollection  5   	   4    .Task_Software.SideWindowCollection.YesNoWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:Task_Software.SideWindowCollection     5   	     � Title$  EditDate��5   	   $	��200��6   $	��300��6'    � WindowStyle$
 None=�63    � AllowsTransparency .  � ShowInTaskbar .   � WindowStartupLocation$ CenterScreen=�6�   ��6`   Z� .��5	      �  � RowDefinitions 5
   
   �� 5      $��40 �6   �� 5       � ColumnDefinitions 5   
   �� 5      $��70 �6   �� 5      $��* �6   �� 5      $��70 �6   .+�5   
   �� $��Center=�6   $��1��5      $��Bottom=�5      ._�5      � -   5         �Name$ draggrid��5      $��34��6%   ���	#FF17A0FF$��0,0,0,0q�6H   .+�5      �� -   5         ��Name$ image��5      $&�� /Task Manager;component/mmmm.png��6'   	 J� OpacityMask
	 �����6Q   5      5      �� 5   
   $	��Top=�6   $��1��5      ._�5      �� -   5        
 ��Name$
 canclebutton��5      $��No��5       $
��0,10q�5!      $��14(�5"         CloseButtonA#��[  5#      67   5%      �� 5&   
   $��2��6   $��1��5'      $	��Top=�5(      ._�5)      �� -   5        $
 
savebutton��5)      $	��Yes��5*      $
��0,10q�5+      $��14(�5,        
Bluebutton#��[ 5.      65   52      �� 53   
   ���	#FF1B6480$��2q�55      $��1��56      $	��Top=�57      ���	#FF2D2D30$��1��58   $   ._�5:      �� -   5         ��Name$ showcausetextblock��5:      
�������$
��Wrap=�5;      $��10q�5<      5A      �� 5B   
   -   5         ��Name$ plzwaitview��5B      $��1��5C      5E      5F      5G      