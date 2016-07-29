// Nome do Campo do Interaction Type onde vai ser colocado o valor passado a partir do dashboard
// Concatenado com o valor do campo pretendido resultante da linha da listagem onde foi executada a ação
var fields = '';

// Alterar o Url ('/mymis/nf_test09/ProcessInteraction/Create/?ProcessTypeCode=Process1&InteractionTypeCode=Process1Interaction1') pelo link para o formulário criado
response.send([{
	StatusCode: 'OK',
	StatusMessage: '',
	Result: {
		Actions : 
		{
			'URL': {
				Location: '/mymis/nf_test09/ProcessInteraction/Create/?ProcessTypeCode=Process1&InteractionTypeCode=Process1Interaction1' + encodeURIComponent(fields),
				Target: 'CURRENT'
			}
		}
	}
}]);