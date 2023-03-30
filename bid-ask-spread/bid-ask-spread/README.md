
# Projeto - Calculo Spread
 
# Converter Log para Txt e substituir 


# Sobre os arquivos no diretório ArquivosCSV

- O arquivo log_completo.txt é a conversão do seprador \u0001 para ; do arquivo original fix.051.incr.log
    - No código não cheguei a uma maneira eficiente de interpretar o separador, sendo assim a conversão é feita via terminal com o comando: cat fix.051.incr.log | tr '\01' ';' | less >> log_completo.txt | A partir do arquivo log_completo.txt o programa é executado.
- O arquivo registrosComTag48.csv é o arquivo que contem toda a serie dos instrumentos 3809779, 3803947, 3809688, 3809654 e 3805660
- O arquivo registrosPorSecIDSelecionado.csv é o arquivo gerado somente com o intrumento selecionado no inicio do programa no prompt conforme opções abaixo:
    - 1 - 3809779
    - 2 - 3803947
    - 3 - 3809688
    - 4 - 3809654
    - 5 - 3805660

