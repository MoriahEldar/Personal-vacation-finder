   M S B A M L   `   `   `  ���� @  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF  #    PLWPF.UpdateHostingUnitWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     %localclr-namespace:PLWPF       � Title$  UpdateHostingUnitWindow��$	��700��$
��1150�� � WindowStartupLocation$ CenterScreen=�.��� .+��� -    ��Name$ label��$��	Unit name��   GeneralLabel#��[  $	��161��$��	35,-9,0,0q��� -   $ label_Copy10��$��Owner��#��[  $	��161��$��	35,44,0,0q��� -   $ 
label_Copy��$��Number of rooms��#��[  $	��231��$��	35,99,0,0q��� -   $ label_Copy1��$��Number of adults beds��#��[  $	��293��$��
35,157,0,0q��� -   $ label_Copy2��$��Number of children beds��#��[  $	��317��$��
35,215,0,0q��� -   $ label_Copy3��$��Number of cribs��#��[  $	��231��$��
35,273,0,0q��� -    ��Name$ textBox��  GeneralText#��[ $��	357,8,0,0q����� *HostingUnitName+ �� Mode$ TwoWay=� �� ValidatesOnExceptions .  �� NotifyOnValidationError .  �� ValidatesOnDataErrors . �� -   $ textBox_Copy��#��[ $��357,114,0,0q����� *
NumRooms+$ TwoWay=� .  .  . �� -	   $ textBox_Copy1��#��[ $��357,172,0,0q����� *
AdultsBeds+$ TwoWay=� .  .  . �� -
   $ textBox_Copy2��#��[ $��357,232,0,0q����� *ChildrenBeds+$ TwoWay=� .  .  . �� -   $ textBox_Copy3��#��[ $��357,288,0,0q����� *Cribs+$ TwoWay=� .  .  . �� -    ��Name$ OwnerComboBox��  GeneralCombo#��[ $��
357,59,0,0q����� �� -   	 ��Name$	 button��$��Update��  GeneralButton#��[ $��765,568,0,0q��� -   
 ��Name$
 richTextBox��$
��Left=�$	��190��$	��Top=�$	��412��$��680,350,0,0q�$��Segoe Print)�. �/� .T�J� .*�.*��� ��� *Description+$ TwoWay=� .  .  . �� -   $ label_Copy11��$��Description��#��[  $	��158��$��680,292,0,0q��� -   $ label_Copy4��$
��Area��#��[  $	��100��$��
35,388,0,0q��� -   $ label_Copy5��$
��Type��#��[  $	��100��$��
35,446,0,0q��� -   $ label_Copy6��$��Pool?��#��[  $	��100��$��
35,504,0,0q��� -   $ label_Copy7��$��Garden?��#��[  $	��161��$��
35,562,0,0q��� -   $ label_Copy8��$��Children attractions?��#��[  $	��275��$��35,625,0,-10.5q��� -   $ AreaComboBox��#��[ $��357,405,0,0q�{�.   �� SelectedItem �� *Area+���� �� -   $ TypeComboBox��#��[ $��357,461,0,0q�{�.   �� *Type+���� �� -    ��Name$ checkBox��$
��Left=�$��357,527,0,0q�$	��Top=�$��33��$��31�� d� 	IsChecked �� *Pool+�� -   $ 	checkBox1��$
��Left=�$��357,588,0,0q�$	��Top=�$��25��$��31�� �� *Garden+�� -   $ 	checkBox2��$
��Left=�$��357,647,0,-0.5q�$	��Top=�$��26��$��31�� �� *ChildrenAttractions+�� -   $ label_Copy12��$��Unit address��#��[  $	��161��$��
35,330,0,0q��� -   $ textBox_Copy4��#��[ $��357,345,0,0q�{�.  ���� *	Address+$ oneWay=� .  .  . �� -   $ label_Copy9��$��In each room��$��12(�#��[  $	��121��$��
35,247,0,0q�$��35��
��������� -   $ label_Copy13��$��In each room��$��12(�#��[  $	��121��$��
35,187,0,0q�$��35��
��������� -   $ label_Copy14��$��Totaly��$��12(�#��[  $	��121��$��
35,303,0,0q�$��35��
�������