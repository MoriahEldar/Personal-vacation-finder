   M S B A M L   `   `   `  ���� @  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF      PLWPF.AddHost   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     %localclr-namespace:PLWPF     .��� .+��� -     ��Name$  label��$��
First name��   GeneralLabel#��[  $	��147��$��	35,20,0,0q��� -   $  
Label_Copy��$��	Last name��#��[  $	��147��$��	35,78,0,0q��� -   $  Label_Copy9��$��ID��#��[  $��
35,136,0,0q��� -   $  Label_Copy1��$��Phone number��#��[  $	��194��$��
35,194,0,0q��� -   $  Label_Copy2��$��Collection clearance��#��[  $	��272��$��
36,382,0,0q��� -   $  Label_Copy3��$��Mail password��#��[  $	��194��$��
35,320,0,0q��� -   $  Label_Copy5��$��Mail Address��#��[  $	��196��$��
35,257,0,0q��� -   $  Label_Copy6��$��	Bank name��#��[  $	��196��$��
465,17,0,0q��� -	   $  Label_Copy7��$��Branch name��#��[  $	��196��$��
465,80,0,0q��� -
   $  Label_Copy8��$��Bank account number��#��[  $	��302��$��465,143,0,0q��� -    ��Name$ textBox��  GeneralText#��[ $��
234,34,0,0q����� *	FirstName+ �� Mode$ TwoWay=� �� ValidatesOnExceptions .  �� NotifyOnValidationError .  �� ValidatesOnDataErrors . �� -   $ textBox_Copy��#��[ $��
234,97,0,0q����� *
LastName+$ TwoWay=� .  .  . �� -   $ textBox_Copy4��#��[ $��234,151,0,0q����� *Id+$ TwoWay=� .  .  . �� -   $ textBox_Copy1��#��[ $��234,212,0,0q����� *PhoneNumber+$ TwoWay=� .  .  . �� -   $ textBox_Copy2��#��[ $��234,274,0,0q����� *Mail+$ TwoWay=� .  .  . �� -   $ bankAccountText��#��[ $��765,159,0,0q����� *BankAccountNumber+$ TwoWay=� .  .  . G� -    G�Name$ passwordBox��    PLWPF.PasswordBoxAssistant   BindPassword . $
��Left=�$��234,337,0,0q�$	��Top=�$��23��$	��156��   BoundPassword �� *
Password+$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=��� -   
 ��Name$
 checkBox��$
��Left=�$	��Top=�$��290,402,0,0q�$��19��$��18�� d� 	IsChecked �� *CollectionClearance+�� -    ��Name$ bankNameCombo��  GeneralCombo#��[ $��
765,34,0,0q� �� SelectedItem �� *
BankName+$ TwoWay=����� �� -   $ branchNameCombo��{�.  #��[ $��765,97,49,0q�$��Stretch=�$
��Auto�� �� *BranchAddress+$ TwoWay=����� �� -    ��Name$ button��$	��Add��  GeneralButton#��[ $��573,243,0,0q�