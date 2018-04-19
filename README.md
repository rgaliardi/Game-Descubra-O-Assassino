# game-DescubraOAssassino
Hands-on

O JOGO O empresário Sean Bean foi assassinado e o corpo dele foi deixado na frente da delegacia de polícia. O Inspetor Jacques Clouseau foi escolhido para investigar este caso. Após uma série de investigações, ele organizou uma lista com prováveis assassinos, os locais do crime e quais armas poderiam ter sido utilizadas. 
	Suspeitos: 
		1. Esqueleto
		2. Khan
		3. Darth vader
		4. Sideshow Bob
		5. Coringa
		6. Duende Verde
		
		Locais: 
		1. Etérnia
		2. Vulcano
		3. Tatooine
		4. Springfield
		5. Gotham
		6. Nova York
		7. Sibéria
		8. Machu Picchu
		9. Show do Katinguele
		10. São Paulo
		
		Armas: 
		1. Cajado Devastador
		2. Phaser
		3. Peixeira
		4. Trezoitão
		5. Sabre de Luz
		6. Bomba
 
Uma testemunha foi encontrada, mas ela só consegue responder se Clouseau fornecer uma teoria. 
Para cada teoria ele "chuta" um assassino, um local e uma arma. 
A testemunha então responde com um número.  
	Se a teoria estiver correta (assassino, local e arma corretos), ela responde 0. 
	Se a teoria está errada, um valor 1, 2 ou 3 é retornado.  
		 1 indica que o assassino está incorreto;  
		 2 indica que o local está incorreto;  
		 3 indica que a arma está incorreta.  
		 Se mais de uma suposição está incorreta, ela retorna um valor arbitrário entre as que estão incorretos (isso é totalmente aleatório). Por exemplo, se o assassino for Khan em Springfield usando uma Peixeira: 
			 Teoria: 1, 1, 1 (Esqueleto, Etérnia, Cajado Devastador)                                                                                    
				o Retorno: 1, ou 2, ou 3 (todos estão incorretos)  
			 Teoria: 6, 4, 5 (Duende Verde, Springfield, Sabre de Luz) 
				o Retorno: 1, ou 3 (somente o local está correto) 
			 Teoria: 5, 4, 3 (Coringa, Springfield, Peixeira) 
				o Retorno: 1 (somente o assassino está incorreto) 
			 Teoria: 2, 4, 3 
				o Retorno: 0 (todos corretos, você solucionou o caso) 
 
Você precisa desenvolver uma solução que simule a testemunha para ajudar Clouseau a resolver o caso, seguindo as seguintes regras: 
	 Ao iniciar o jogo o sistema “sorteia” um crime, escolhendo um suspeito, um local e uma arma aleatoriamente.   
	 O usuário assume o papel do Inspetor Jacques Clouseau e “interroga” a testemunha sugerindo uma teoria (Suspeito, Local e Arma) 
	 O sistema deve então confrontar a teoria do Inspetor Closeau (usuário) com o crime atual (sorteado ao iniciar o jogo) e responder uma das três alternativas descritas: 
		 Assassino incorreto  
		 Local incorreto 
		 Arma incorreta 
	 O jogo continua com o usuário enviando novas “teorias” até que o crime seja desvendado 
	 Quando o inspetor Clouseau descobrir o suspeito o local e a arma acaba o jogo, dando os parabéns pela solução do caso e perguntar se ele quer investigar um novo “crime”. 
 
ESPECIFICAÇÕES 
 
	 A entrega esperada é: o Um desenho da sugestão de arquitetura para navegador e para app mobile o Descrição dos motivos que levaram às escolhas das ferramentas ou técnicas sugeridas  o Uma POC funcional do jogo para navegador   Linguagem C#   MVC (respeitando padrões de desenvolvimento) ou SPA  Padrões de arquitetura de sua escolha (SOA, DDD, Microserviços)  Framework javascript para o front-end de sua escolha (vue, knockout, angular, etc) 
 
	 A sugestão de tecnologias para Armazenamento de Dados, Backend, Frontend e Mobile são livres.  
 
	 A metodologia de desenvolvimento e boas práticas de mercado também são livres. 
 
	 O formato da entrega final deve ser o envio do link do projeto no GitHub contendo os entregáveis acima  
 
