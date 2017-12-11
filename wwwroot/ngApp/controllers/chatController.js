class ChatController {
    constructor($scope) {
        this.messages=[];
        this.message="";
        this.connection = new signalR.HubConnection('/chat');
        this.connection.start().then(() => this.connection.invoke('send', 'Hello'));
        this.connection.on('send', data => {
                    this.messages.push(data);
                    $scope.$apply();
                });
        
       
    }
    sendMessage() {
        this.connection.invoke('send', this.message);
    }
}