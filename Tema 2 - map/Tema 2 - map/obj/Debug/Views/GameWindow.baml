   M S B A M L   `   `   `  ����G  CTema 2 - map, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Tema_2___map.ViewModelsTema_2___map.ViewModels  5   	   #    Tema_2___map.Views.GameWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <
viewModels%clr-namespace:Tema_2___map.ViewModels     5   	     � Title$  Checkers Game��5      ���	#FF121212$	��700��5
   	   $	��900��6    � WindowStartupLocation$ CenterScreen=�6"   .��5      � .+�5   
   �� $��Turn: ��6   $	��Top=�6"   $
��Left=�6:   $��10q�6U   $��20(�6a    � DataContext 5      $  Tema_2___map.ViewModels.GameVM  5      ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value
 �����63    �� Triggers 5      �� 5       �� Binding 6&   �� *CurrentTurn+ �� Value Red6F   .a�5      ��  � ��
 �  ��6;   5      �� 5       6&   �� *CurrentTurn+	 Black6F   .a�5      ��  � ��
 �����6;   5      5      5!      �� 5&   
   ���	#FF121212$��Center=�6(   $��Center=�6C   ._�5'      � $	��490��6   $	��490��6   .+�5+      �� 
���   �$��5q�6-   ��5,      �� 5-      	 �� ImageSource$D	 >D:/Facultate/MAP/Tema 2 - map/Tema 2 - map/Resources/Board.jpg��6%   
 j� Stretch$
 UniformToFill=�6r    50        51      ._�54      �� -   5        $
��grid��54   #   $��Center=�6Q   $��Center=�6l   $	��478��6�   $	��478��6�   ��6/   �� *	GameBoard+��55      �� 56      .R�57   "   �� ��6/   �� ��58   &   �� 59   *   .R�5:   .   ��  �� Orientation$ 
Horizontal=�69   5;   +   ��5=   &   �� 5>   *   .R�5?   .   �� $��50��6   $��50��6�   $��5q�6�   $��0q�6�   ��65   �� *ClickCommand+��6V   �� *SimplePiece+.��5@   2   �� $
��Fill=�6b   ��68   �� *SimplePiece.PieceImage+6b   ��5A   2   �� 5B   6   " ��6<   .�5C   :   ��  � ��
 ���� 6W   5D   7   5F   /   5G   +   5I   #   5J      5L      5M      5N      5O      5P      5Q      