using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    public class GetVideosListQueryHandler : IRequestHandler<GetVideosListQuery, List<VideoVM>>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public GetVideosListQueryHandler(IVideoRepository videoRepository, IMapper mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public async Task<List<VideoVM>> Handle(GetVideosListQuery request, CancellationToken cancellationToken)
        {
           var videoList = await _videoRepository.GetVideoByUsername(request.Username);

            return _mapper.Map<List<VideoVM>>(videoList);
        }




    }
}
