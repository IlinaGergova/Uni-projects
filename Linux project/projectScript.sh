#пренасочва файлът към местоположението на интерпретатора 
#!/bin/sh

#меню с поле за въвеждане на името на файла с ключовите думи
file_name=$(dialog --clear --title "$Processkiller" --inputbox "Enter file name: " 10 20 2>&1 >/dev/tty)

#изчиства екрана
clear

#цикъл,итериращ всеки ред във файла с ключовите думи, -r предотвратява интрепретирането на blackslash escapes, а IFS предотвратява изрязването на разстоянието в началото и края 
while IFS='' read -r line
do

#запазва текущите процеси във файл
        ps aux > helpFile
        if
        #проверява дали думата на текущия ред я има във файла с текущите процеси, -q предотвратява принтенето на резултата от командата
                grep -q "$line" helpFile
        then
        #ако да, то се появява меню с два избора"да" или "не" за потвърждение дали да бъде изтрит/бъдат изтрити намерените процеси
                options=(1 "Yes" 2 "No")
                choice=$(dialog --clear --title "$Processkiller" --menu "Do you want to kill the process '$line': " 20 20 2 "${options[@]}" 2>&1 >/dev/tty)
                clear
                #при избор на "да", се изтрива процеса/процесите
                case $choice in
                        1)
                                pkill -f "$line"
                                ;;
        
                esac
                
        else
        #ако не, то се появява съответното съобщение
                echo "no such process '$line'"
        fi
done < "$file_name"
exit 0

