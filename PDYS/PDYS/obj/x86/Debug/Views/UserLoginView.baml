   M S B A M L   `   `   `  ����  PDYS    PDYS.Views.UserLoginView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^DevExpress.Xpf.Core.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      $	��350��5      $��80��5      .��5
      �   � ColumnDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��72��6   $��72��5      $��
10,10,10,0q�5      $��Center=�5      $	��Top=�5      $)��#/PDYS;component/Images/security.png��5      $
��Fill=�5      � 5   
   $��1��6   $��0,0,0,0q�6     5      �� 5      $
��Auto �6#   �� 5      $��* �6#    � RowDefinitions 5!      �� 5"      $
��Auto �6    �� 5#      $
��Auto �6    �� 5$      $��* �6    .+�5)      �� $��0��6   $��0��5*      $��Center=�5+      $��Kullanıcı Adı��5,      %  DevExpress.Xpf.Editors.TextEdit  5.      -   5          Name$ txtUserName��5.      $��0��5/      $��1��50      $��	10,5,10,5q�51      $��Stretch=�52      $��Center=�53      %  DevExpress.Xpf.Editors.BaseEdit   	EditValue 54      �� *
UserName+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors . �� 58      $��1��6   $��0��59      $��Center=�5:      $��Parola��5;      ,  &DevExpress.Xpf.Editors.PasswordBoxEdit  5=      -   5          Name$ txtPassword��5=   "   $��1��5>   "   $��1��5?   "   $��	10,5,10,5q�5@   "    5A   "   �� *
Password+$ TwoWay=� . 5E      5I      5J      