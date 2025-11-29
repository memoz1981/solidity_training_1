pragma solidity ^0.8.30;
// SPDX-License-Identifier: UNLICENSED

contract Voter {
    uint[] private votes;
    string[] private options; 
    mapping(address => bool) hasVoted; 

    constructor(string[] memory _options)
    {
        require(_options.length > 1, "Two or more options should be available");
        options = _options;
        votes = new uint[](_options.length); 
    } 

    function vote(uint optionIndex) public {
        require(optionIndex < options.length, "Invalid option index"); 
        require(!hasVoted[msg.sender], "You have already voted"); 
        recordVote(optionIndex);
    }

    function recordVote(uint optionIndex) private {
        hasVoted[msg.sender] = true; 
        votes[optionIndex] = votes[optionIndex] + 1; 
    }

    function getOptions() public view returns (string[] memory) {
        return options; 
    }

    function getVotes() public view returns(uint[] memory)
    {
        return votes;
    }
}