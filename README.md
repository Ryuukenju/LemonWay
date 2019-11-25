--26/11/2019
La solution est constinuer de 3 projets:
1 - un WebService "LemonWayServices"
2 - Une Applciation console "ConsolApp" qui consomme le web service "ConsolApp"
3 - Une Application WinForm qui consomme le web service "LemonWayFormProjet"

- Les deux projets "LemonWayServices" et "LemonWayFormProjet" sont démarrées en lancant la solution, pour ConsolApp il faut le lancer à part si vous voulez le tester.

Instructions:
1 - Lancer le web service "LemonWayServices", et récuperer le lien localhost:...asmx.
1 - mettre le bon lien du webservice dans la class "GlobalConfig" (LemonWayServices => GlobalConfig) et modifier la variable _url_WebService.
2 - Lancer la solution.
