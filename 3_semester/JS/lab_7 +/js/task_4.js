let team = {
    players: [
        {name: "BlankBS", role: "Нападющий"},
        {name: "KostusLi", role: "Вратарь"},
        {name: "Vitalik", role: "Защитник"}
    ],

    getPlayers(){
        this.players.forEach(player => {
            console.log(`имя: ${player.name}, позиция: ${player.role}`);
        });
    }
};

team.getPlayers();