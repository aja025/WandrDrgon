class GridController {
    constructor($window, $scope, $timeout, $accountService, $profileService) {
        this.timeout = $timeout;
        this.window = $window;
        this.scope = $scope;
        this.dragon = "images//diggingdragon.png";
        this.background = "images//newSprites//black.png";
        this.yum = "images//diggingdragongold.png";
        this.board_size = 21;
        this.directions = { left: 37, up: 38, right: 39, down: 40 };
        this.colors = { fruit: this.yum, head: this.dragon, board: this.background};
        this.fruit = { 
                        x: this.fruitX, 
                        y: this.fruitY };
        
        
        this.users = 0;
        this.profileId = $accountService.getClaim("profileId");
        this.profile = $profileService.getProfile(this.profileId);
        

        this.setUpBoard();

        this.setStyling = (col, row) => {
            if (this.snake.x == row && this.snake.y == col) {
                return this.colors.head;
            } else if (this.fruitX == row && this.fruitY == col) {
                return this.colors.fruit;
            }
            return this.colors.board;
        };
        this.messages = [];
        this.message = "";
        this.names = [];
        this.connection = new signalR.HubConnection('/multiplayer');

        this.connection.start().then(() => {this.resetFruit();
                                            this.connection.invoke("joined", this.profile.displayName)});

        this.connection.on('all', (f, r) => {
            this.fruitX = f;
            this.fruitY = r;
            
            $scope.$apply();
        });
        this.connection.on('send', (name, data) => {
            this.messages.push(name);
            this.messages.push(data);
            $scope.$apply();

        });
        this.connection.on('count', count => {
            this.users = count;
            $scope.$apply();

        });
        this.connection.on('joined', name => {
            this.messages.push(name);
            $scope.$apply();
        });
        this.startGame();

        $window.addEventListener("keyup", e => {

            if (e.keyCode === this.directions.left && this.snake.direction !== this.directions.right) {
                this.snake.direction = this.directions.left;
            } else if (e.keyCode === this.directions.up && this.snake.direction !== this.directions.down) {
                this.snake.direction = this.directions.up;
            } else if (e.keyCode === this.directions.right && this.snake.direction !== this.directions.left) {
                this.snake.direction = this.directions.right;
            } else if (e.keyCode === this.directions.down && this.snake.direction !== this.directions.up) {
                this.snake.direction = this.directions.down;
            }
        })

    }
    sendMessage() {
        this.connection.invoke('send',this.profile.displayName, this.message);
        document.getElementById('chatinput').value = "";

    }

    setUpBoard() {
        this.board = [];
        for (var i = 0; i < this.board_size; i++) {
            this.board[i] = [];
            for (var j = 0; j < this.board_size; j++) {
                this.board[i][j] = false;
            }
        }
    }

    startGame() {
        this.score = 0;

        this.x = Math.floor(Math.random() * this.board_size);
        this.y = Math.floor(Math.random() * this.board_size);

        this.snake = {
            direction: this.directions.left,
            x: this.x,
            y: this.y
        };
        this.interval = 250;

        this.resetFruit();
        this.update();
    }

    fruitCollision(part) {
        if(part.x === parseInt(this.fruitX) && part.y === parseInt(this.fruitY) ){
           return true; 
        }else{
            return false;
        }
        
    }

    resetFruit() {
        this.f = Math.floor((Math.random() * this.board_size-1) + 1);
        this.t = Math.floor((Math.random() * this.board_size-1) + 1);

        
        // if (this.board[parseInt(this.f)][parseInt(this.t)] == true) {
        //     return this.resetFruit();
        // }
        this.connection.invoke('all', this.f, this.t);
        

    }

    eatFruit() {
        this.score++;
        this.window.sessionStorage.setItem("gridScore", this.score);


        this.resetFruit();

        if (this.score % 5 === 0) {
            this.interval -= 15;
        }
    }

    update() {
        this.newHead = this.getdirection();
        // console.log(this.fruitCollision(this.newHead));
        if (this.fruitCollision(this.newHead) == true) {
            this.eatFruit();
        }
        if (this.newHead.x > 20) {
            this.newHead.x = 0;
        }
        if (this.newHead.y > 20) {
            this.newHead.y = 0;
        }
        if (this.newHead.x < 0) {
            this.newHead.x = 20;
        }
        if (this.newHead.y < 0) {
            this.newHead.y = 20;
        }
        this.connection.invoke('count');
        this.timeout(() => { this.update(); }, this.interval);

    }

    getdirection() {

        if (this.snake.direction === this.directions.left) {
            this.snake.x -= 1;
        } else if (this.snake.direction === this.directions.right) {
            this.snake.x += 1;
        } else if (this.snake.direction === this.directions.up) {
            this.snake.y -= 1;
        } else if (this.snake.direction === this.directions.down) {
            this.snake.y += 1;
        }
        return this.snake;

    }
    check(e){
        if (e.keyCode === 13){
            this.sendMessage();
        }
    }

}