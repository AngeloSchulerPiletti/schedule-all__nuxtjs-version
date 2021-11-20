const TaskToken = artifacts.require("TaskToken");

contract("TaskToken", accounts => {
  it("should have a message", () =>
    TaskToken.deployed()
      .then(instance => instance.message.call())
      .then(message => {
          console.log(`mensagem é: ${message}`);
        assert.equal(
          message,
          'Heyoo',
          "wrong message"
        );
      })
    );
});