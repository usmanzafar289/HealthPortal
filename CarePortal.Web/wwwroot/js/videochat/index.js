'use strict';

//var Video = require('twilio-video');
var Video = Twilio.Video;

var activeRoom;
var previewTracks;
var identity;
var roomName;


// Attach the Track to the DOM.
function attachTrack(track, container) {
    debugger;
    container.appendChild(track.attach());
}

// Attach array of Tracks to the DOM.
function attachTracks(tracks, container) {
    tracks.forEach(function (track) {
        attachTrack(track, container);
    });
}

// Detach given track from the DOM
function detachTrack(track) {
    track.detach().forEach(function (element) {
        element.remove();
    });
}

// A new RemoteTrack was published to the Room.
function trackPublished(publication, container) {
    debugger;
    if (publication.isSubscribed) {
        debugger;
        attachTrack(publication.track, container);
    }
    publication.on('subscribed', function (track) {
        debugger;
        log('Subscribed to ' + publication.kind + ' track');
        attachTrack(track, container);
    });
    publication.on('unsubscribed', detachTrack);
}

// A RemoteTrack was unpublished from the Room.
function trackUnpublished(publication) {
    log(publication.kind + ' track was unpublished.');
}

// A new RemoteParticipant joined the Room
function participantConnected(participant, container) {
    participant.tracks.forEach(function (publication) {
        debugger;
        trackPublished(publication, container);
    });
    participant.on('trackPublished', function (publication) {
        debugger;
        trackPublished(publication, container);
    });
    participant.on('trackUnpublished', trackUnpublished);
}

// Detach the Participant's Tracks from the DOM.
function detachParticipantTracks(participant) {
    var tracks = getTracks(participant);
    tracks.forEach(detachTrack);
}

// When we are about to transition away from this page, disconnect
// from the room, if joined.
window.addEventListener('beforeunload', leaveRoomIfJoined);

// Obtain a token from the server in order to connect to the Room.
$.getJSON(AppUrl + '/VideoChat/GetToken/', function (data) {
    identity = data.identity;
    document.getElementById('room-controls').style.display = 'block';

    // Bind button to join Room.
    document.getElementById('button-join').onclick = function () {
        roomName = document.getElementById('room-name').value;
        if (!roomName) {
            alert('Please enter a room name.');
            return;
        }

        log("Joining room '" + roomName + "'...");
        var connectOptions = {
            name: roomName,
            logLevel: 'debug'
        };

        if (previewTracks) {
            connectOptions.tracks = previewTracks;
        }

        // Join the Room with the token from the server and the
        // LocalParticipant's Tracks.
        Video.connect(data.token, connectOptions).then(roomJoined, function (error) {
            log('Could not connect to Twilio: ' + error.message);
        });
    };

    // Bind button to leave Room.
    document.getElementById('button-leave').onclick = function () {
        log('Leaving room...');
        activeRoom.disconnect();
    };
});

// Get the Participant's Tracks.
function getTracks(participant) {
    return Array.from(participant.tracks.values()).filter(function (publication) {
        return publication.track;
    }).map(function (publication) {
        return publication.track;
    });
}

// Successfully connected!
function roomJoined(room) {
    debugger;
    //window.room = activeRoom = room;

    //log("Joined as '" + identity + "'");
    //document.getElementById('button-join').style.display = 'none';
    //document.getElementById('button-leave').style.display = 'inline';

    //// Attach LocalParticipant's Tracks, if not already attached.
    //var previewContainer = document.getElementById('local-media');
    //if (!previewContainer.querySelector('video')) {
    //    attachTracks(getTracks(room.localParticipant), previewContainer);
    //}

    //// Attach the Tracks of the Room's Participants.
    //var remoteMediaContainer = document.getElementById('remote-media');

    //room.participants.forEach(function (participant) {
    //    debugger;
    //    log("Already in Room: '" + participant.identity + "'");
    //    participantConnected(participant, remoteMediaContainer);
    //});

    //// When a Participant joins the Room, log the event.
    //room.on('participantConnected', function (participant) {
    //    debugger;
    //    log("Joining: '" + participant.identity + "'");
    //    participantConnected(participant, remoteMediaContainer);
    //});

    //// Attach the Participant's Media to a <div> element.
    //room.on('participantConnected', participant => {
    //    console.log('Participant "${participant.identity}" connected');

    //    participant.tracks.forEach(publication => {
    //        debugger;
    //        if (publication.isSubscribed) {
    //            debugger;
    //            const track = publication.track;
    //            document.getElementById('remote-media-div').appendChild(track.attach());
    //        }
    //    });

    //    participant.on('trackSubscribed', track => {
    //        debugger;
    //        document.getElementById('remote-media-div').appendChild(track.attach());
    //    });
    //});

    window.room = activeRoom = room;
    log("Joined as '" + identity + "'");
   
    document.getElementById('button-join').style.display = 'none';
    document.getElementById('button-leave').style.display = 'inline';

    const remoteDiv = document.getElementById('remote-media');
    const localDiv = document.getElementById('local-media');

    if (!localDiv.querySelector('video')) {
        room.localParticipant.tracks.forEach(track => {
            localDiv.appendChild(track.attach());
        });
    }
    
    //room.localParticipant.tracks.forEach(track => {
    //    localDiv.appendChild(track.attach());
    //});
        
    room.on('participantConnected', participant => {
        debugger;
        // 3. Handle any Tracks already present in a Participant's `tracks` Map.
        participant.tracks.forEach(track => {
            remoteDiv.appendChild(track.attach());
        });

        // 4. Handle any Tracks added after the fact via the "trackAdded" event.
        participant.on('trackAdded', track => {
            remoteDiv.appendChild(track.attach());
        });
    });

    room.participants.forEach(participant => {
        debugger;
        // 3. Handle any Tracks already present in a Participant's `tracks` Map.
        participant.tracks.forEach(track => {
            remoteDiv.appendChild(track.attach());
        });

        // 4. Handle any Tracks added after the fact via the "trackAdded" event.
        participant.on('trackAdded', track => {
            remoteDiv.appendChild(track.attach());
        });
    });
    // When a Participant leaves the Room, detach its Tracks.
    room.on('participantDisconnected', function (participant) {
        log("RemoteParticipant '" + participant.identity + "' left the room");
        detachParticipantTracks(participant);
    });

    // Once the LocalParticipant leaves the room, detach the Tracks
    // of all Participants, including that of the LocalParticipant.
    room.on('disconnected', function () {
        log('Left');
        if (previewTracks) {
            previewTracks.forEach(function (track) {
                track.stop();
            });
            previewTracks = null;
        }
        detachParticipantTracks(room.localParticipant);
        room.participants.forEach(detachParticipantTracks);
        activeRoom = null;
        document.getElementById('button-join').style.display = 'inline';
        document.getElementById('button-leave').style.display = 'none';
    });
}

//const { createLocalVideoTrack } = Twilio.Video;//require('twilio-video');

//createLocalVideoTrack().then(track => {
//    const localMediaContainer = document.getElementById('local-media');
//    localMediaContainer.appendChild(track.attach());
//});

// Preview LocalParticipant's Tracks.
document.getElementById('button-preview').onclick = function () {
    var localTracksPromise = previewTracks
        ? Promise.resolve(previewTracks)
        : Video.createLocalTracks();

    localTracksPromise.then(function (tracks) {
        window.previewTracks = previewTracks = tracks;
        var previewContainer = document.getElementById('local-media');
        if (!previewContainer.querySelector('video')) {
            attachTracks(tracks, previewContainer);
        }
    }, function (error) {
        console.error('Unable to access local media', error);
        log('Unable to access Camera and Microphone');
    });
};


// Activity log.
function log(message) {
    var logDiv = document.getElementById('log');
    logDiv.innerHTML += '<p>&gt;&nbsp;' + message + '</p>';
    logDiv.scrollTop = logDiv.scrollHeight;
}

// Leave Room.
function leaveRoomIfJoined() {
    if (activeRoom) {
        activeRoom.disconnect();
    }
}
